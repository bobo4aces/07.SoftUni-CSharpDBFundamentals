using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    public class Bet
    {
        public Bet()
        {

        }

        public Bet(decimal amount, string prediction, DateTime dateTime, User user, Game game)
        {
            this.Amount = amount;
            this.Prediction = prediction;
            this.DateTime = dateTime;
            this.User = user;
            this.Game = game;
        }

        [Key]
        public int BetId { get; set; }

        [Required]
        [Column("Amount", TypeName = "money")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(1)]
        [Column("Prediction", TypeName = "char(1)")]
        public string Prediction { get; set; }

        [Required]
        [Column("DateTime", TypeName = "date")]
        public DateTime DateTime { get; set; }

        [Required]
        [Column("UserId", TypeName = "int")]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        [Column("GameId", TypeName = "int")]
        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }
    }
}
