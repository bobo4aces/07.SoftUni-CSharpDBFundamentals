using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    [Table("Courses")]
    public class Course
    {
        public Course()
        {

        }

        public Course(int courseId, string name, string description, DateTime startDate, DateTime endDate, decimal price)
        {
            this.CourseId = courseId;
            this.Name = name;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Price = price;
        }

        [Key]
        public int CourseId { get; set; }

        [Required]
        [StringLength(80)]
        [Column("Name", TypeName = "nvarchar(80)")]
        public string Name { get; set; }

        [Column("Description", TypeName = "ntext")]
        public string Description { get; set; }

        [Required]
        [Column("StartDate", TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("EndDate", TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Column("Price", TypeName = "money")]
        public decimal Price { get; set; }

        public ICollection<Resource> Resources { get; set; }
        public ICollection<Homework> HomeworkSubmissions { get; set; }
        public ICollection<StudentCourse> StudentsEnrolled { get; set; }
    }
}
