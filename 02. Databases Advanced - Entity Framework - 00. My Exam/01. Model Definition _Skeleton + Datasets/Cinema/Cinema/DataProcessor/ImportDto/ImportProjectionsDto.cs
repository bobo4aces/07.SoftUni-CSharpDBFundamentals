using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Projection")]
    public class ImportProjectionsDto
    {
        //    <Projection>
        //<MovieId>6</MovieId>
        //<HallId>4</HallId>
        //<DateTime>2019-05-12 05:51:29</DateTime>
        [XmlElement("MovieId")]
        [Required]
        public int MovieId { get; set; }
        [XmlElement("HallId")]
        [Required]
        public int HallId { get; set; }
        [XmlElement("DateTime")]
        [Required]
        public string DateTime { get; set; }
    }
}
