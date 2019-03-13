using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data.Configurations
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudentId);
            builder.Property(s => s.Name)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .IsUnicode(true);
            builder.Property(s => s.PhoneNumber)
                .IsRequired(false)
                .IsFixedLength(true)
                .HasMaxLength(10)
                .HasColumnName("PhoneNumber")
                .HasColumnType("char")
                .IsUnicode(false);
            builder.Property(s => s.RegisteredOn)
                .IsRequired(true)
                .HasColumnName("RegisteredOn")
                .HasColumnType("date");
            builder.Property(s => s.Birthday)
                .IsRequired(false)
                .HasColumnName("Birthday")
                .HasColumnType("date");
        }
    }
}
