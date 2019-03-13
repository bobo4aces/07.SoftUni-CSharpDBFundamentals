using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_FootballBetting.Data.Models
{
    public class Position
    {
        public Position()
        {

        }

        public Position(string name)
        {
            this.Name = name;
            this.Players = new List<Player>();
        }

        [Key]
        public int PositionId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Name", TypeName = "varchar(50)")]
        public string Name { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
