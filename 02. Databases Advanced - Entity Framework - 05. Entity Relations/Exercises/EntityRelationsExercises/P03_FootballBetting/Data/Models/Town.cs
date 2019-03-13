using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    public class Town
    {
        public Town()
        {

        }

        public Town(string name, Country country)
        {
            this.Name = name;
            this.Country = country;
            this.Teams = new List<Team>();
        }

        [Key]
        public int TownId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Name", TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column("CountryId", TypeName = "int")]
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }

        public ICollection<Team> Teams { get; set; }
    }
}
