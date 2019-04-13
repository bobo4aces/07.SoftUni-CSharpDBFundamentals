using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Cinema.DataProcessor.ImportDto
{
    [XmlType("Ticket")]
    public class ImportTicketDto
    {
        //  <Ticket>
        //    <ProjectionId>1</ProjectionId>
        //    <Price>7</Price>
        [XmlElement("ProjectionId")]
        [Required]
        public int ProjectionId { get; set; }
        [XmlElement("Price")]
        [Required]
        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }
    }
}