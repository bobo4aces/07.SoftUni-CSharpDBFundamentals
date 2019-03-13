using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data.Configurations
{
    public class HomeworkConfig : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasKey(h => h.HomeworkId);
            builder.Property(h => h.Content)
                .IsRequired(true)
                .HasColumnName("Content")
                .HasColumnType("text")
                .IsUnicode(false);
            builder.Property(h => h.ContentType)
                .IsRequired(true)
                .HasMaxLength(11)
                .HasColumnName("ContentType")
                .HasColumnType("varchar")
                .IsUnicode(false);
            builder.Property(h => h.SubmissionTime)
                .IsRequired(true)
                .HasColumnName("SubmissionTime")
                .HasColumnType("date");
            builder.Property(h => h.StudentId)
                .IsRequired(true)
                .HasColumnName("StudentId")
                .HasColumnType("int");
            builder.Property(h => h.CourseId)
                .IsRequired(true)
                .HasColumnName("CourseId")
                .HasColumnType("int");

            builder
                .HasOne(h => h.Student)
                .WithMany(h => h.HomeworkSubmissions)
                .HasForeignKey(h => h.StudentId);

            builder
                .HasOne(h => h.Course)
                .WithMany(h => h.HomeworkSubmissions)
                .HasForeignKey(h => h.CourseId);
        }
    }
}
