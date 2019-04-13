namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Cinema.Data.Models;
    using Cinema.Data.Models.Enums;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie 
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat 
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection 
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket 
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            ImportMoviesDto[] importMoviesDtos = JsonConvert.DeserializeObject<ImportMoviesDto[]>(jsonString);
            List<Movie> movies = new List<Movie>();
            StringBuilder sb = new StringBuilder();
            foreach (var importMoviesDto in importMoviesDtos)
            {
                if (!IsValid(importMoviesDto)||movies.Any(m=>m.Title == importMoviesDto.Title))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Movie movie = new Movie()
                {
                    Director = importMoviesDto.Director,
                    Genre = Enum.Parse<Genre>(importMoviesDto.Genre),
                    Duration = TimeSpan.Parse(importMoviesDto.Duration),
                    Rating = importMoviesDto.Rating,
                    Title = importMoviesDto.Title
                };
                movies.Add(movie);
                sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre, movie.Rating.ToString("0.00")));
            }

            context.Movies.AddRange(movies);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            ImportHallsAndSeatsDto[] importHallsAndSeats = JsonConvert.DeserializeObject<ImportHallsAndSeatsDto[]>(jsonString);
            List<Hall> halls = new List<Hall>();
            StringBuilder sb = new StringBuilder();

            foreach (var importHallsAndSeat in importHallsAndSeats)
            {
                if (!IsValid(importHallsAndSeat) || importHallsAndSeat.Seats <= 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                List<Seat> seats = new List<Seat>();
                for (int i = 0; i < importHallsAndSeat.Seats; i++)
                {
                    seats.Add(new Seat());
                }
                Hall hall = new Hall()
                {
                    Is3D = importHallsAndSeat.Is3D,
                    Is4Dx = importHallsAndSeat.Is4Dx,
                    Name = importHallsAndSeat.Name,
                    Seats = seats
                };
                halls.Add(hall);
                string projectionType = string.Empty;

                if (hall.Is4Dx && hall.Is3D)
                {
                    projectionType += "4Dx/3D";
                }
                else if (hall.Is4Dx)
                {
                    projectionType += "4Dx";
                }
                else if (hall.Is3D)
                {
                    projectionType += "3D";
                }
                else
                {
                    projectionType += "Normal";
                }
                sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, projectionType, hall.Seats.Count));
            }
            context.Halls.AddRange(halls);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportProjectionsDto[]), new XmlRootAttribute("Projections"));
            ImportProjectionsDto[] importProjections = (ImportProjectionsDto[])xmlSerializer.Deserialize(new StringReader(xmlString));
            List<int> hallIds = context.Halls.Select(h => h.Id).ToList();
            List<int> movieIds = context.Movies.Select(m => m.Id).ToList();
            var movies = context.Movies.Select(m => new { m.Id, m.Title }).ToList();
            List<Projection> projections = new List<Projection>();
            StringBuilder sb = new StringBuilder();

            foreach (var importProjection in importProjections)
            {
                if (!IsValid(importProjection) || !hallIds.Contains(importProjection.HallId) || !movieIds.Contains(importProjection.MovieId))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Projection projection = new Projection()
                {
                    DateTime = DateTime.Parse(importProjection.DateTime, CultureInfo.InvariantCulture),
                    HallId = importProjection.HallId,
                    MovieId = importProjection.MovieId
                };
                projections.Add(projection);
                string title = movies.FirstOrDefault(m => m.Id == projection.MovieId).Title;
                sb.AppendLine(string.Format(SuccessfulImportProjection, title, projection.DateTime.ToString("MM/dd/yyyy")));
            }

            context.Projections.AddRange(projections);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportCustomersAndTicketsDto[]), new XmlRootAttribute("Customers"));
            ImportCustomersAndTicketsDto[] importCustomersAndTickets = (ImportCustomersAndTicketsDto[])xmlSerializer.Deserialize(new StringReader(xmlString));
            HashSet<Customer> customers = new HashSet<Customer>();
            List<Projection> projections = context.Projections.ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var customer in importCustomersAndTickets)
            {
                if (!IsValid(customer) || !customer.Tickets.All(IsValid))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                HashSet<Projection> currentProjections = new HashSet<Projection>();
                HashSet<Ticket> tickets = new HashSet<Ticket>();
                if (customers.Any(c=>c.FirstName == customer.FirstName)&&
                    customers.Any(c => c.LastName == customer.LastName) &&
                    customers.Any(c => c.Balance == customer.Balance) &&
                    customers.Any(c => c.Age == customer.Age))
                {
                    continue;
                }
                foreach (var ticket in customer.Tickets)
                {
                    Projection projection = projections.FirstOrDefault(p => p.Id == ticket.ProjectionId);
                    if (projection == null)
                    {
                        continue;
                    }
                    Ticket currentTicket = new Ticket()
                    {
                        Price = ticket.Price,
                        ProjectionId = ticket.ProjectionId
                    };
                    if (tickets.Any(t=>t.ProjectionId == ticket.ProjectionId)&&tickets.Any(t=>t.Price == ticket.Price))
                    {
                        continue;
                    }
                    projection.Tickets.Add(currentTicket);
                    tickets.Add(currentTicket);
                    currentProjections.Add(projection);
                }
                Customer currentCustomer = new Customer()
                {
                    Age = customer.Age,
                    Balance = customer.Balance,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    //Tickets = currentProjections.SelectMany(p => p.Tickets).ToArray()
                    Tickets = currentProjections.SelectMany(p => p.Tickets.Select(x => new Ticket()
                    {
                        ProjectionId = x.ProjectionId,
                        Price = x.Price
                    })).ToArray()
                };
                
                customers.Add(currentCustomer);
                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, currentCustomer.FirstName, currentCustomer.LastName, currentCustomer.Tickets.Count));

            }
            
            context.Customers.AddRange(customers);
            
            context.SaveChanges();
            
            return sb.ToString().TrimEnd();
            
        }
       
        private static bool IsValid(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}