using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Data.Models
{
    public class Seat
    {
        //        •	Id – integer, Primary Key
        //•	HallId – integer, foreign key(required)
        //•	Hall – the seat’s hall
        [Key]
        public int Id { get; set; }
        [Required]
        public int HallId { get; set; }
        [ForeignKey(nameof(HallId))]
        public Hall Hall { get; set; }
    }
}