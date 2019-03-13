using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    public class Game
    {
        public Game()
        {

        }
        public Game(Team homeTeam, 
            Team awayTeam, 
            int homeTeamGoals, 
            int awayTeamGoals, 
            DateTime dateTime, 
            double homeTeamBetRate, 
            double awayTeamBetRate, 
            double drawBetRate, 
            string result)
        {
            this.HomeTeam = homeTeam;
            this.AwayTeam = awayTeam;
            this.HomeTeamGoals = homeTeamGoals;
            this.AwayTeamGoals = awayTeamGoals;
            this.DateTime = dateTime;
            this.HomeTeamBetRate = homeTeamBetRate;
            this.AwayTeamBetRate = awayTeamBetRate;
            this.DrawBetRate = drawBetRate;
            this.Result = result;
            this.PlayerStatistics = new List<PlayerStatistic>();
            this.Bets = new List<Bet>();
    }

        [Key]
        public int GameId { get; set; }

        [Required]
        [Column("HomeTeamId", TypeName = "int")]
        public int HomeTeamId { get; set; }

        [ForeignKey(nameof(HomeTeamId))]
        public Team HomeTeam { get; set; }

        [Required]
        [Column("AwayTeamId", TypeName = "int")]
        public int AwayTeamId { get; set; }

        [ForeignKey(nameof(AwayTeamId))]
        public Team AwayTeam { get; set; }

        [Required]
        [Column("HomeTeamGoals", TypeName = "int")]
        public int HomeTeamGoals { get; set; }

        [Required]
        [Column("AwayTeamGoals", TypeName = "int")]
        public int AwayTeamGoals { get; set; }

        [Required]
        [Column("DateTime", TypeName = "date")]
        public DateTime DateTime { get; set; }

        [Required]
        [Column("HomeTeamBetRate", TypeName = "decimal(15,2)")]
        public double HomeTeamBetRate { get; set; }

        [Required]
        [Column("AwayTeamBetRate", TypeName = "decimal(15,2)")]
        public double AwayTeamBetRate { get; set; }

        [Required]
        [Column("DrawBetRate", TypeName = "decimal(15,2)")]
        public double DrawBetRate { get; set; }

        [Required]
        [StringLength(5)]
        [Column("Result", TypeName = "varchar(5)")]
        public string Result { get; set; }

        public ICollection<PlayerStatistic> PlayerStatistics { get; set; }
        public ICollection<Bet> Bets { get; set; }
    }
}
