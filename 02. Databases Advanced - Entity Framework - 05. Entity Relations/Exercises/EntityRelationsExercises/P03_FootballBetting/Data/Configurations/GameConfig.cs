using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.GameId);
            builder.Property(g => g.HomeTeamId)
                .IsRequired(true)
                .HasColumnName("HomeTeamId")
                .HasColumnType("int");
            builder.Property(g => g.AwayTeamId)
                .IsRequired(true)
                .HasColumnName("AwayTeamId")
                .HasColumnType("int");
            builder.Property(g => g.HomeTeamGoals)
                .IsRequired(true)
                .HasColumnName("HomeTeamGoals")
                .HasColumnType("int");
            builder.Property(g => g.AwayTeamGoals)
                .IsRequired(true)
                .HasColumnName("AwayTeamGoals")
                .HasColumnType("int");
            builder.Property(g => g.DateTime)
                .IsRequired(true)
                .HasColumnName("DateTime")
                .HasColumnType("date");
            builder.Property(g => g.HomeTeamBetRate)
                .IsRequired(true)
                .HasColumnName("HomeTeamBetRate")
                .HasColumnType("decimal(15,2)");
            builder.Property(g => g.AwayTeamBetRate)
                .IsRequired(true)
                .HasColumnName("AwayTeamBetRate")
                .HasColumnType("decimal(15,2)");
            builder.Property(g => g.DrawBetRate)
                .IsRequired(true)
                .HasColumnName("DrawBetRate")
                .HasColumnType("decimal(15,2)");
            builder.Property(g => g.Result)
                .IsRequired(true)
                .HasMaxLength(5)
                .IsFixedLength(false)
                .HasColumnName("Result")
                .HasColumnType("varchar")
                .IsUnicode(false);

            builder
                .HasMany(g => g.PlayerStatistics)
                .WithOne(g => g.Game)
                .HasForeignKey(g => g.PlayerId);
            builder
                .HasMany(g => g.Bets)
                .WithOne(g => g.Game)
                .HasForeignKey(g => g.BetId);
            builder
                .HasOne(g => g.HomeTeam)
                .WithMany(g => g.HomeGames)
                .HasForeignKey(g => g.HomeTeamId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(g => g.AwayTeam)
                .WithMany(g => g.AwayGames)
                .HasForeignKey(g => g.AwayTeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
