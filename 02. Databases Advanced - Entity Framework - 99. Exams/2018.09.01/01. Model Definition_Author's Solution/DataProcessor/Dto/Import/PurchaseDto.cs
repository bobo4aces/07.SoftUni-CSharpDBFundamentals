namespace VaporStore.DataProcessor.Dto.Import
{
	using System.ComponentModel.DataAnnotations;
	using System.Xml.Serialization;
	using Data.Models.Enums;

	[XmlType("Purchase")]
	public class PurchaseDto
	{
		[XmlAttribute("title")]
		public string Title { get; set; }

		public PurchaseType Type { get; set; }

		[RegularExpression(@"^[\dA-Z]{4}-[\dA-Z]{4}-[\dA-Z]{4}$")]
		public string Key { get; set; }

		public string Card { get; set; }

		public string Date { get; set; }
	}
}