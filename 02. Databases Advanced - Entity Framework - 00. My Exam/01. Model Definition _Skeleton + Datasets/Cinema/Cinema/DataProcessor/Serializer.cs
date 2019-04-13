namespace Cinema.DataProcessor
{
    using System;
    using System.Linq;
    using Cinema.DataProcessor.ExportDto;
    using Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportTopMovies(CinemaContext context, int rating)
        {
            ExportTopMoviesDto[] exportTopMovies = context.Movies
                .Where(m => m.Rating >= rating && m.Projections.Any(p => p.Tickets.Count >= 1))
                .Select(m => new ExportTopMoviesDto()
                {
                    MovieName = m.Title,
                    Rating = m.Rating.ToString("0.00"),
                    TotalIncomes = m.Projections.SelectMany(p => p.Tickets).Sum(t => t.Price).ToString("0.00"),
                    Customers = m.Projections.SelectMany(p => p.Tickets).Select(p => new ExportCustomerDto()
                    {
                        FirstName = p.Customer.FirstName,
                        LastName = p.Customer.LastName,
                        Balance = p.Customer.Balance.ToString("0.00")
                    })
                    //Check
                    .OrderByDescending(c=>c.Balance)
                    .ThenBy(c=>c.FirstName)
                    .ThenBy(c=>c.LastName)
                    .ToArray()
                })
                .Take(10)
                .OrderByDescending(m=>m.Rating)
                .ThenByDescending(m=>m.TotalIncomes)
                .ToArray();
            string json = JsonConvert.SerializeObject(exportTopMovies);
            return json;
        }

        public static string ExportTopCustomers(CinemaContext context, int age)
        {
            throw new NotImplementedException();
        }
    }
}