using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data.Configurations
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.CourseId);
            builder.Property(c => c.Name)
                .IsRequired(true)
                .HasMaxLength(80)
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .IsUnicode(true);
            builder.Property(c => c.Description)
                .IsRequired(false)
                .HasColumnName("Description")
                .HasColumnType("ntext")
                .IsUnicode(true);
            builder.Property(c => c.StartDate)
                .IsRequired(true)
                .HasColumnName("StartDate")
                .HasColumnType("date");
            builder.Property(c => c.EndDate)
                .IsRequired(true)
                .HasColumnName("EndDate")
                .HasColumnType("date");
            builder.Property(c => c.Price)
                .IsRequired(true)
                .HasColumnName("Price")
                .HasColumnType("money");
        }
    }
}
