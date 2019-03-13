using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    public class PlayerStatistic
    {
        public PlayerStatistic()
        {

        }
        public PlayerStatistic(Game game, Player player, int scoredGoals, int assists, int minutesPlayed)
        {
            this.Game = game;
            this.Player = player;
            this.ScoredGoals = scoredGoals;
            this.Assists = assists;
            this.MinutesPlayed = minutesPlayed;
        }

        [Required]
        [Column("GameId", TypeName = "int")]
        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; }

        [Required]
        [Column("PlayerId", TypeName = "int")]
        public int PlayerId { get; set; }

        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }

        [Required]
        [Column("ScoredGoals", TypeName = "int")]
        public int ScoredGoals { get; set; }

        [Required]
        [Column("Assists", TypeName = "int")]
        public int Assists { get; set; }

        [Required]
        [Column("MinutesPlayed", TypeName = "int")]
        public int MinutesPlayed { get; set; }
    }
}
