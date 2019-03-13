using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_StudentSystem.Data.Configurations
{
    public class ResourceConfig : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(r=>r.ResourceId);
            builder.Property(r => r.Name)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .IsUnicode(true);
            builder.Property(r => r.Url)
                .IsRequired(true)
                .HasColumnName("Url")
                .HasColumnType("text")
                .IsUnicode(false);
            builder.Property(r => r.ResourceType)
                .IsRequired(true)
                .HasMaxLength(12)
                .HasColumnName("ResourceType")
                .HasColumnType("varchar")
                .IsUnicode(false);
            builder.Property(r => r.CourseId)
                .IsRequired(true)
                .HasColumnName("CourseId")
                .HasColumnType("int");

            builder.HasOne(c => c.Course)
                .WithMany(r => r.Resources)
                .HasForeignKey(c => c.CourseId);
        }
    }
}
