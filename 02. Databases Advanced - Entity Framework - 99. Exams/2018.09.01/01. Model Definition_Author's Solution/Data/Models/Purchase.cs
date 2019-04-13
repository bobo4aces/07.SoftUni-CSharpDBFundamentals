namespace VaporStore.Data.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Enums;

	public class Purchase
	{
		public Purchase()
		{
		}

		public Purchase(Game game, PurchaseType type, Card card, string productKey, DateTime date)
		{
			this.Game = game;
			this.Type = type;
			this.Card = card;
			this.ProductKey = productKey;
			this.Date = date;
		}

		[Key]
		public int Id { get; set; }

		public PurchaseType Type { get; set; }

		[RegularExpression(@"^[\dA-Z]{4}-[\dA-Z]{4}-[\dA-Z]{4}$")]
		public string ProductKey { get; set; }

		public DateTime Date { get; set; }

		[ForeignKey(nameof(Card))]
		public int CardId { get; set; }

		public Card Card { get; set; }

		[ForeignKey(nameof(Game))]
		public int GameId { get; set; }

		public Game Game { get; set; }
	}
}