using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportMailsDto
    {
        //  {
        //    "Description": "please add me to your LinkedIn network",
        //    "Sender": "Zonda Vasiljevic",
        //    "Address": "51677 Rieder Center str."
        //  }
        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z 0-9]+ str\.$")]
        public string Address { get; set; }
    }
}