using System.Xml.Serialization;

namespace VaporStore.Datasets.Dto
{
    public class ExportXmlGameDto
    {
        //    <Game title = "Counter-Strike: Global Offensive" >
        //      < Genre > Action </ Genre >
        //      < Price > 12.49 </ Price >
        //    </ Game >

        [XmlAttribute("title")]
        public string Title { get; set; }
        [XmlElement("Genre")]
        public string Genre { get; set; }
        [XmlElement("Price")]
        public string Price { get; set; }
    }
}