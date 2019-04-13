using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Data.Models
{
    public class Projection
    {
        //        •	Id – integer, Primary Key
        //•	MovieId – integer, foreign key(required)
        //•	Movie – the projection’s movie
        //•	HallId – integer, foreign key(required)
        //•	Hall – the projection’s hall 
        //•	DateTime - DateTime(required)
        //•	Tickets - collection of type Ticket
        public Projection()
        {
            this.Tickets = new List<Ticket>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        [ForeignKey(nameof(MovieId))]
        public Movie Movie { get; set; }
        [Required]
        public int HallId { get; set; }
        [ForeignKey(nameof(HallId))]
        public Hall Hall { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}