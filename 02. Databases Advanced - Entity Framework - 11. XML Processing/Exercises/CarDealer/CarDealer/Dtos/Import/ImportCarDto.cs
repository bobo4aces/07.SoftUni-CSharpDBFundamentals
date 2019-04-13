using CarDealer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.Dtos.Import
{
    [XmlType("Car")]
    public class ImportCarDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TraveledDistance { get; set; }

        [XmlArray("parts")]
        [XmlArrayItem("partId", Type = typeof(ImportPartIdDto))]
        public ImportPartIdDto[] Parts { get; set; }
    }

    [XmlType("partId")]
    //[XmlInclude(typeof(ImportCarDto))]
    public class ImportPartIdDto
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
    }
}
