using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VaporStore.Data.Models
{
    public class Developer
    {
        //        •	Id – integer, Primary Key
        //•	Name – text(required)
        //•	Games - collection of type Game
        public Developer()
        {
            this.Games = new List<Game>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}