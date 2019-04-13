using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class ImportOfficersPrisonersDto
    {
        //    <Officer>
        //<Name>Minerva Holl</Name>
        //<Money>2582.55</Money>
        //<Position>Overseer</Position>
        //<Weapon>ChainRifle</Weapon>
        //<DepartmentId>2</DepartmentId>
        //<Prisoners>
        //  <Prisoner id = "15" />
        //</ Prisoners >
        [XmlElement("Name")]
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [XmlElement("Money")]
        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public string Money { get; set; }
        [Required]
        [XmlElement("Position")]
        public string Position { get; set; }
        [Required]
        [XmlElement("Weapon")]
        public string Weapon { get; set; }
        [Required]
        [XmlElement("DepartmentId")]
        public string DepartmentId { get; set; }
        [XmlArray("Prisoners")]
        public ImportPrisonerDto[] Prisoners { get; set; }
    }
}
