using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class PlayerConfig : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.PlayerId);
            builder.Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsFixedLength(false)
                .HasColumnName("Name")
                .HasColumnType("varchar")
                .IsUnicode(false);
            builder.Property(p => p.SquadNumber)
                .IsRequired(true)
                .HasColumnName("SquadNumber")
                .HasColumnType("int");
            builder.Property(p => p.TeamId)
                .IsRequired(true)
                .HasColumnName("TeamId")
                .HasColumnType("int");
            builder.Property(p => p.PositionId)
                .IsRequired(true)
                .HasColumnName("PositionId")
                .HasColumnType("int");
            builder.Property(p => p.IsInjured)
                .IsRequired(true)
                .HasColumnName("IsInjured")
                .HasColumnType("bit");

            builder
                .HasMany(p => p.PlayerStatistics)
                .WithOne(p => p.Player)
                .HasForeignKey(p => p.PlayerId);
            builder
                .HasOne(p => p.Team)
                .WithMany(p => p.Players)
                .HasForeignKey(p => p.TeamId);
            builder
                .HasOne(p => p.Position)
                .WithMany(p => p.Players)
                .HasForeignKey(p => p.PositionId);
        }
    }
}
