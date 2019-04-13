using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportDepartmentDto
    {
        //  {
        //    "CellNumber": 101,
        //    "HasWindow": true
        //  },
        [Required]
        [Range(typeof(int), "1", "1000")]
        public int CellNumber { get; set; }
        [Required]
        public bool HasWindow { get; set; }
    }
}