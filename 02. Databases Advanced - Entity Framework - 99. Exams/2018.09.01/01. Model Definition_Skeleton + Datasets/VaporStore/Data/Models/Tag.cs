using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VaporStore.Data.Models
{
    public class Tag
    {
        public Tag()
        {
            this.GameTags = new List<GameTag>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Name { get; set; }

        public ICollection<GameTag> GameTags { get; set; }
    }
}
