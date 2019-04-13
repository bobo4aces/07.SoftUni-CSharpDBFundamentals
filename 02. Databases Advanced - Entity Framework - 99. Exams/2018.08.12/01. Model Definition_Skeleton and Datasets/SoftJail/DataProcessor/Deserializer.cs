namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string errorMessage = "Invalid Data";
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            ImportDepartmentsCellsDto[] departmentsCellsDtos = JsonConvert.DeserializeObject<ImportDepartmentsCellsDto[]>(jsonString);
            List<Department> departments = new List<Department>();
            List<Cell> cells = new List<Cell>();

            StringBuilder sb = new StringBuilder();
            foreach (var departmentsCellsDto in departmentsCellsDtos)
            {
                if (!IsValid(departmentsCellsDto)||!departmentsCellsDto.Cells.All(IsValid))
                {
                    sb.AppendLine(errorMessage);
                    continue;
                }
                List<Cell> currentCells = new List<Cell>();
                foreach (var departmentsCells in departmentsCellsDto.Cells)
                {
                    Cell cell = cells.FirstOrDefault(c => c.CellNumber == departmentsCells.CellNumber);
                    if (cell == null)
                    {
                        cell = new Cell()
                        {
                            CellNumber = departmentsCells.CellNumber,
                            HasWindow = departmentsCells.HasWindow
                        };
                        cells.Add(cell);
                    }
                    currentCells.Add(cell);
                }
                
                Department department = new Department()
                {
                    Name = departmentsCellsDto.Name,
                    Cells = currentCells
                };
                departments.Add(department);
                sb.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");
            }
            context.Departments.AddRange(departments);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            ImportPrisonersMailsDto[] prisonersMailsDtos = JsonConvert.DeserializeObject<ImportPrisonersMailsDto[]>(jsonString);
            List<Prisoner> prisoners = new List<Prisoner>();

            StringBuilder sb = new StringBuilder();
            foreach (var prisonersMailsDto in prisonersMailsDtos)
            {
                if (!IsValid(prisonersMailsDto) || !prisonersMailsDto.Mails.All(IsValid))
                {
                    sb.AppendLine(errorMessage);
                    continue;
                }
                DateTime? releaseDate = null;
                if (prisonersMailsDto.ReleaseDate != null)
                {
                    releaseDate = DateTime.ParseExact(prisonersMailsDto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                Prisoner prisoner = new Prisoner()
                {
                    Age = prisonersMailsDto.Age,
                    Bail = prisonersMailsDto.Bail == null ? 0 : prisonersMailsDto.Bail,
                    CellId = prisonersMailsDto.CellId,
                    FullName = prisonersMailsDto.FullName,
                    IncarcerationDate = DateTime.ParseExact(prisonersMailsDto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Mails = prisonersMailsDto.Mails
                            .Select(m=>new Mail()
                            {
                                Address = m.Address,
                                Description = m.Description,
                                Sender = m.Sender
                            })
                            .ToArray(),
                    Nickname = prisonersMailsDto.Nickname,
                    ReleaseDate = releaseDate
                };
                prisoners.Add(prisoner);
                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
            }
            context.Prisoners.AddRange(prisoners);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportOfficersPrisonersDto[]), new XmlRootAttribute("Officers"));
            ImportOfficersPrisonersDto[] officersPrisonersDtos = (ImportOfficersPrisonersDto[])xmlSerializer.Deserialize(new StringReader(xmlString));
            List<Officer> officers = new List<Officer>();

            List<int> prisonerIds = context.Prisoners.Select(p => p.Id).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var officersPrisonersDto in officersPrisonersDtos)
            {
                bool isValidWeapon = Enum.TryParse<Weapon>(officersPrisonersDto.Weapon, out Weapon weapon);
                bool isValidPosition = Enum.TryParse<Position>(officersPrisonersDto.Position, out Position position);
                if (!IsValid(officersPrisonersDto) || !isValidWeapon || !isValidPosition)
                {
                    sb.AppendLine(errorMessage);
                    continue;
                }
                
                Officer officer = new Officer()
                {
                    DepartmentId = int.Parse(officersPrisonersDto.DepartmentId),
                    FullName = officersPrisonersDto.Name,
                    Position = position,
                    Salary = decimal.Parse(officersPrisonersDto.Money),
                    Weapon = weapon,
                    OfficerPrisoners = officersPrisonersDto.Prisoners
                                    .Select(p=> new OfficerPrisoner
                                    {
                                         PrisonerId = int.Parse(p.Id)
                                    })
                                    .ToArray()
                };
                officers.Add(officer);
                sb.AppendLine($"Imported {officer.FullName} ({officer.OfficerPrisoners.Count} prisoners)");
            }
            context.Officers.AddRange(officers);
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