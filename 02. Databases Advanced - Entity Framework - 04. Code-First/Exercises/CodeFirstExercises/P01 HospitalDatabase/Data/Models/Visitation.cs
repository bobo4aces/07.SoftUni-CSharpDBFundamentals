using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_HospitalDatabase.Data.Models
{
    [Table("Visitations")]
    public class Visitation
    {
        public Visitation()
        {

        }
        public Visitation(DateTime date, string comments, Patient patient, Doctor doctor)
        {
            this.Date = date;
            this.Comments = comments;
            this.Patient = patient;
            this.Doctor = doctor;
        }
        [Key]
        public int VisitationId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(250)]
        public string Comments { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        
    }
}
