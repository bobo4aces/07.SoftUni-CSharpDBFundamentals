using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.DataProcessor.ImportDto
{
    public class ImportHallsAndSeatsDto
    {
        //      "Name": "Methocarbamol",
        //  "Is4Dx": false,
        //  "Is3D": true,
        //  "Seats": 52
        //},
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Name { get; set; }
        public bool Is4Dx { get; set; }
        public bool Is3D { get; set; }
        [Required]
        public int Seats { get; set; }
    }
}
