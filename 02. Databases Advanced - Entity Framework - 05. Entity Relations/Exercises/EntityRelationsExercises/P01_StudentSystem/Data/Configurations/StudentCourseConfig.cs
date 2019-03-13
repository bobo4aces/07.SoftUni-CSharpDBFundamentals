using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data.Configurations
{
    public class StudentCourseConfig : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasKey(sc => new { sc.StudentId, sc.CourseId });
            builder
                .HasOne(sc => sc.Student)
                .WithMany(sc => sc.CourseEnrollments)
                .HasForeignKey(sc => sc.StudentId);
            builder
                .HasOne(sc => sc.Course)
                .WithMany(sc => sc.StudentsEnrolled)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}
