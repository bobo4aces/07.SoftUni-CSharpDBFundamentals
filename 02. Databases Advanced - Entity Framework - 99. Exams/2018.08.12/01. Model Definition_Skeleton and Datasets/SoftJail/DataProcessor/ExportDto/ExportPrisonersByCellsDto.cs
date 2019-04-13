using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.DataProcessor.ExportDto
{
    public class ExportPrisonersByCellsDto
    {
        //    "Id": 3,
        //"Name": "Binni Cornhill",
        //"CellNumber": 503,
        //"Officers": [
        //  {
        //    "OfficerName": "Hailee Kennon",
        //    "Department": "ArtificialIntelligence"
        //  },
        //  {
        //    "OfficerName": "Theo Carde",
        //    "Department": "Blockchain"
        //  }
        //],
        //"TotalOfficerSalary": 7127.93
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CellNumber { get; set; }
        public ExportOfficerDto[] Officers { get; set; }
        public decimal TotalOfficerSalary { get; set; }
    }
}
