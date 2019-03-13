using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class DiagnoseConfig : IEntityTypeConfiguration<Diagnose>
    {
        public void Configure(EntityTypeBuilder<Diagnose> builder)
        {
            builder
                .HasKey(d => d.DiagnoseId);
            builder.Property(d => d.Name)
                .IsRequired(true)
                .HasColumnType("nvarchar")
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsUnicode(true);
            builder.Property(d => d.Comments)
                .IsRequired(false)
                .HasColumnType("nvarchar")
                .HasColumnName("Comments")
                .HasMaxLength(250)
                .IsUnicode(true);
            builder
                .HasOne(d => d.Patient)
                .WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.PatientId);
        }
    }
}
