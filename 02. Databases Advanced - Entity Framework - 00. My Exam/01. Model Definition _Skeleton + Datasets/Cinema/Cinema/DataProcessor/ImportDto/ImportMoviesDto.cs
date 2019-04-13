using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.DataProcessor.ImportDto
{
    public class ImportMoviesDto
    {
        //    "Title": "F/X2 (a.k.a. F/X 2 - The Deadly Art of Illusion)",
        //"Genre": "Action",
        //"Duration": "01:57:00",
        //"Rating": 7,
        //"Director": "Sheppard Cescoti"

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public string Duration { get; set; }
        [Required]
        [Range(typeof(double), "1", "10")]
        public double Rating { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Director { get; set; }
    }
}
