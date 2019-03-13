using P01_StudentSystem.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P01_StudentSystem.Data.Models
{
    [Table("HomeworkSubmissions")]
    public class Homework
    {
        public Homework()
        {

        }

        public Homework(int homeworkId, string content, ContentType contentType, DateTime submissionTime, Student student, Course course)
        {
            this.HomeworkId = homeworkId;
            this.Content = content;
            this.ContentType = contentType;
            this.SubmissionTime = submissionTime;
            this.Student = student;
            this.Course = course;
        }

        [Key]
        public int HomeworkId { get; set; }

        [Required]
        [Column("Content", TypeName = "text")]
        public string Content { get; set; }

        [Required]
        [StringLength(11)]
        [Column("ContentType",TypeName = "varchar(11)")]
        public ContentType ContentType { get; set; }

        [Required]
        [Column("SubmissionTime", TypeName = "date")]
        public DateTime SubmissionTime { get; set; }

        [Required]
        [Column("StudentId", TypeName = "int")]
        public int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }

        [Required]
        [Column("CourseId", TypeName = "int")]
        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
    }
}
