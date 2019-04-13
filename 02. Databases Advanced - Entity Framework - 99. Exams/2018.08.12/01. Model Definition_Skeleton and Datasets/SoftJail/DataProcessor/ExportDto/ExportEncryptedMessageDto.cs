using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Message")]
    public class ExportEncryptedMessageDto
    {
        //  <EncryptedMessages>
        //    <Message>
        //      <Description>!?sdnasuoht evif-ytnewt rof deksa uoy ro orez artxe na ereht sI</Description>
        //    </Message>
        //  </EncryptedMessages>
        [XmlElement("Description")]
        public string Description { get; set; }
    }
}