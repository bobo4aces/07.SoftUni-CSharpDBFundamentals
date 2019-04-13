using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftJail.Data.Models
{
    public class OfficerPrisoner
    {
        //        •	PrisonerId – integer, Primary Key
        //•	Prisoner – the officer’s prisoner(required)
        //•	OfficerId – integer, Primary Key
        //•	Officer – the prisoner’s officer(required)

        public int PrisonerId { get; set; }
        [Required]
        [ForeignKey(nameof(PrisonerId))]
        public Prisoner Prisoner { get; set; }
        public int OfficerId { get; set; }
        [Required]
        [ForeignKey(nameof(OfficerId))]
        public Officer Officer { get; set; }
    }
}