using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VaporStore.Data.Models
{
    public class User
    {
        //        •	Id – integer, Primary Key
        //•	Username – text with length[3, 20] (required)
        //•	FullName – text, which has two words, consisting of Latin letters.Both start with an upper letter and are separated by a single space(ex. "John Smith") (required)
        //•	Email – text(required)
        //•	Age – integer in the range[3, 103] (required)
        //•	Cards – collection of type Card

        public User()
        {
            this.Cards = new List<Card>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        [StringLength(20,MinimumLength = 3)]
        public string Username { get; set; }
        [Required]
        [Column(TypeName = "varchar(max)")]
        [RegularExpression(@"^[A-Z][a-z]+ [A-Z][a-z]+$")]
        public string FullName { get; set; }
        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Email { get; set; }

        [Required]
        [Range(typeof(int),"3","103")]
        public int Age { get; set; }

        public ICollection<Card> Cards { get; set; }

    }
}
