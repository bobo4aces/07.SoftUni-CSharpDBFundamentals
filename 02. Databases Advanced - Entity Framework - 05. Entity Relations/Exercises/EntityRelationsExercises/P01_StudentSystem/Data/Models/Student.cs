using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    [Table("Students")]
    public class Student
    {
        public Student()
        {

        }
        public Student(int studentId, string name, string phoneNumber, DateTime registeredOn, DateTime birthday)
        {
            this.StudentId = studentId;
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.RegisteredOn = registeredOn;
            this.Birthday = birthday;
        }

        [Key]
        public int StudentId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Name",TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [StringLength(10,MinimumLength = 10)]
        [Column("PhoneNumber", TypeName = "char(10)")]
        public string PhoneNumber { get; set; }
        
        [Required]
        [Column("PhoneNumber", TypeName = "date")]
        public DateTime RegisteredOn { get; set; }

        [Column("Birthday", TypeName = "date")]
        public DateTime? Birthday { get; set; }

        public ICollection<Homework> HomeworkSubmissions { get; set; }
        public ICollection<StudentCourse> CourseEnrollments { get; set; }
    }
}
