using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SoftJail.DataProcessor.ImportDto
{
    public class ImportPrisonersMailsDto
    {
        //    "FullName": "Melanie Simonich",
        //"Nickname": "The Wallaby",
        //"Age": 32,
        //"IncarcerationDate": "29/03/1957",
        //"ReleaseDate": "27/03/2006",
        //"Bail": null,
        //"CellId": 5,
        //"Mails": [
        //  {
        //    "Description": "please add me to your LinkedIn network",
        //    "Sender": "Zonda Vasiljevic",
        //    "Address": "51677 Rieder Center str."
        //  }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^The [A-Z][a-z]+$")]
        public string Nickname { get; set; }
        [Required]
        [Range(typeof(int), "18", "65")]
        public int Age { get; set; }

        [Required]
        public string IncarcerationDate { get; set; }

        public string ReleaseDate { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal? Bail { get; set; }

        public int? CellId { get; set; }
        public ImportMailsDto[] Mails { get; set; }
    }
}
