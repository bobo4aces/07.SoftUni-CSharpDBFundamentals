using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class TownConfig : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> builder)
        {
            builder.HasKey(t => t.TownId);
            builder.Property(t => t.Name)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsFixedLength(false)
                .HasColumnName("Name")
                .HasColumnType("varchar")
                .IsUnicode(false);
            //builder.Property(t => t.CountryId)
            //    .IsRequired(true)
            //    .HasColumnName("CountryId")
            //    .HasColumnType("int");

            builder
                .HasMany(t => t.Teams)
                .WithOne(t => t.Town)
                .HasForeignKey(t => t.TownId);
            builder
                .HasOne(t => t.Country)
                .WithMany(t => t.Towns)
                .HasForeignKey(t => t.CountryId);
        }
    }
}
