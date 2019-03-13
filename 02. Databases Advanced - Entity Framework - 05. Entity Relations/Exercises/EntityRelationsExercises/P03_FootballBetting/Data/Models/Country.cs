using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    public class Country
    {
        public Country()
        {

        }

        public Country(string name)
        {
            this.Name = name;
            this.Towns = new List<Town>();
        }

        [Key]
        public int CountryId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Name", TypeName = "varchar(50)")]
        public string Name { get; set; }

        public ICollection<Town> Towns { get; set; }
    }
}
