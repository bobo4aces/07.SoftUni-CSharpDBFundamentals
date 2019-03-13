using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class PlayerStatisticConfig : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> builder)
        {
            builder.HasKey(ps => new { ps.GameId, ps.PlayerId });
            builder.Property(ps => ps.ScoredGoals)
                .IsRequired(true)
                .HasColumnName("ScoredGoals")
                .HasColumnType("int");
            builder.Property(ps => ps.Assists)
                .IsRequired(true)
                .HasColumnName("Assists")
                .HasColumnType("int");
            builder.Property(ps => ps.MinutesPlayed)
                .IsRequired(true)
                .HasColumnName("MinutesPlayed")
                .HasColumnType("int");

            builder
                .HasOne(ps => ps.Game)
                .WithMany(ps => ps.PlayerStatistics)
                .HasForeignKey(ps => ps.GameId);
            builder
                .HasOne(ps => ps.Player)
                .WithMany(ps => ps.PlayerStatistics)
                .HasForeignKey(ps => ps.PlayerId);
        }
    }
}
