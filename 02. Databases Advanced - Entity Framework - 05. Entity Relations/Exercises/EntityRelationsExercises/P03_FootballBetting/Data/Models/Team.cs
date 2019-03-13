using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    public class Team
    {
        public Team()
        {

        }

        public Team(string name, string logoUrl, string initials, decimal budget, Color primaryKitColor, Color secondaryKitColor, Town town)
        {
            this.Name = name;
            this.LogoUrl = logoUrl;
            this.Initials = initials;
            this.Budget = budget;
            this.PrimaryKitColor = primaryKitColor;
            this.SecondaryKitColor = secondaryKitColor;
            this.Town = town;
            this.HomeGames = new List<Game>();
            this.AwayGames = new List<Game>();
            this.Players = new List<Player>();
        }

        [Key]
        public int TeamId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Name", TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Column("LogoUrl", TypeName = "varchar(50)")]
        public string LogoUrl { get; set; }

        [Required]
        [StringLength(3)]
        [Column("Initials", TypeName = "char(3)")]
        public string Initials { get; set; }

        [Required]
        [Column("Budget", TypeName = "money")]
        public decimal Budget { get; set; }

        [Required]
        public int PrimaryKitColorId { get; set; }

        [ForeignKey(nameof(PrimaryKitColorId))]
        public Color PrimaryKitColor { get; set; }

        [Required]
        public int SecondaryKitColorId { get; set; }

        [ForeignKey(nameof(SecondaryKitColorId))]
        public Color SecondaryKitColor { get; set; }

        [Required]
        public int TownId { get; set; }

        [ForeignKey(nameof(TownId))]
        public Town Town { get; set; }

        public ICollection<Game> HomeGames { get; set; }
        public ICollection<Game> AwayGames { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
