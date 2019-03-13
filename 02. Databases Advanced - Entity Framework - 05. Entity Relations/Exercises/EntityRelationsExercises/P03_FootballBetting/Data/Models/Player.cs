using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    public class Player
    {
        public Player()
        {

        }

        public Player(string name, int squadNumber, Team team, Position position, bool isInjured)
        {
            this.Name = name;
            this.SquadNumber = squadNumber;
            this.Team = team;
            this.Position = position;
            this.IsInjured = isInjured;
            this.PlayerStatistics = new List<PlayerStatistic>();
        }

        [Key]
        public int PlayerId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Name", TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column("SquadNumber", TypeName = "int")]
        public int SquadNumber { get; set; }

        [Required]
        [Column("TeamId", TypeName = "int")]
        public int TeamId { get; set; }

        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; }

        [Required]
        [Column("PositionId", TypeName = "int")]
        public int PositionId { get; set; }

        [ForeignKey(nameof(PositionId))]
        public Position Position { get; set; }

        [Required]
        [Column("IsInjured", TypeName = "bit")]
        public bool IsInjured { get; set; }

        public ICollection<PlayerStatistic> PlayerStatistics { get; set; }
    }
}
