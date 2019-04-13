using System.ComponentModel.DataAnnotations;

namespace VaporStore.Datasets.Dto
{
    public class ImportCardDto
    {
        //    "Number": "1833 5024 0553 6211",
        //    "CVC": "903",
        //    "Type": "Debit"
        [Required]
        [StringLength(19, MinimumLength = 19)]
        [RegularExpression(@"^[0-9]+ [0-9]+ [0-9]+ [0-9]+$")]
        public string Number { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        [RegularExpression(@"^[0-9]{3}$")]
        public string CVC { get; set; }

        [Required]
        public string Type { get; set; }
    }
}