using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder
                .HasKey(p => p.PatientId);
            builder.Property(p => p.FirstName)
                .IsRequired(true)
                .HasColumnName("FirstName")
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsUnicode(true);
            builder.Property(p => p.LastName)
                .IsRequired(true)
                .HasColumnName("LastName")
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsUnicode(true);
            builder.Property(p => p.Address)
                .IsRequired(true)
                .HasColumnName("Address")
                .HasColumnType("nvarchar")
                .HasMaxLength(250)
                .IsUnicode(true);
            builder.Property(p => p.Email)
                .IsRequired(true)
                .HasColumnName("Email")
                .HasColumnType("varchar")
                .HasMaxLength(80)
                .IsUnicode(false);
            builder.Property(p => p.HasInsurance)
                .IsRequired(true)
                .HasColumnName("HasInsurance")
                .HasColumnType("bit");

        }
    }
}
