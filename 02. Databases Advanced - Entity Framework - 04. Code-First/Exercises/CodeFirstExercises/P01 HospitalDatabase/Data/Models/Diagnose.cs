using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_HospitalDatabase.Data.Models
{
    [Table("Diagnoses")]
    public class Diagnose
    {
        public Diagnose()
        {

        }
        public Diagnose(string name, string comments, Patient patient)
        {
            this.Name = name;
            this.Comments = comments;
            this.Patient = patient;
        }
        [Key]
        public int DiagnoseId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Comments { get; set; }
        
        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
