namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            ExportPrisonersByCellsDto[] prisonersByCells = context.Prisoners
                .Where(p => ids.Contains(p.Id))
                .Select(p => new ExportPrisonersByCellsDto
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers.Select(po => new ExportOfficerDto()
                    {
                        OfficerName = po.Officer.FullName,
                        Department = po.Officer.Department.Name
                    })
                    .OrderBy(po => po.OfficerName)
                    .ToArray(),
                    TotalOfficerSalary = p.PrisonerOfficers.Sum(po => po.Officer.Salary)
                })
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToArray();

            string json = JsonConvert.SerializeObject(prisonersByCells, Newtonsoft.Json.Formatting.Indented);

            return json;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            string[] names = prisonersNames
                .Split(",")
                .ToArray();
            ExportPrisonersInboxDto[] exportPrisonersInboxes = context.Prisoners
                .Where(p => names.Contains(p.FullName))
                .Select(p => new ExportPrisonersInboxDto()
                {
                    Id = p.Id,
                    Name = p.FullName,
                    IncarcerationDate = p.IncarcerationDate.ToString("yyyy-MM-dd"),
                    EncryptedMessages = p.Mails.Select(m => new ExportEncryptedMessageDto()
                    {
                        Description = ReverseDescription(m.Description)
                    })
                    .ToArray()
                })
                .OrderBy(p=>p.Name)
                .ThenBy(p=>p.Id)
                .ToArray();

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExportPrisonersInboxDto[]), new XmlRootAttribute("Prisoners"));
            StringBuilder sb = new StringBuilder();
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true,
                OmitXmlDeclaration = true,
            };
            xmlSerializer.Serialize(new StringWriter(sb), exportPrisonersInboxes, namespaces);

            return sb.ToString().TrimEnd();
        }

        private static string ReverseDescription(string description)
        {
            return string.Join("",description.Reverse().ToArray());
        }
    }
}