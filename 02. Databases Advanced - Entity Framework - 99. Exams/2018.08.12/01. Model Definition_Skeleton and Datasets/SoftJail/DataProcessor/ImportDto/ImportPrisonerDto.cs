using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Prisoner")]
    public class ImportPrisonerDto
    {
        //<Prisoners>
        //  <Prisoner id = "15" />
        //</ Prisoners >
        [XmlAttribute("id")]
        [Required]
        public string Id { get; set; }
    }
}