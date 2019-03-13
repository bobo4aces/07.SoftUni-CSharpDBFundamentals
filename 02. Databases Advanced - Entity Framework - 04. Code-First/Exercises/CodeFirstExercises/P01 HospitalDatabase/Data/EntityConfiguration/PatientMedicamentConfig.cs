using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    public class PatientMedicamentConfig : IEntityTypeConfiguration<PatientMedicament>
    {
        public void Configure(EntityTypeBuilder<PatientMedicament> builder)
        {
            builder.HasKey(p => new { p.PatientId, p.MedicamentId });
            builder
                .HasOne(p => p.Patient)
                .WithMany(p => p.Perscriptions)
                .HasForeignKey(p => p.PatientId);
            builder
                .HasOne(p => p.Medicament)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(m => m.MedicamentId);
        }
    }
}
