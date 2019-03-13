using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.TeamId);
            builder.Property(t => t.Name)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsFixedLength(false)
                .HasColumnName("Name")
                .HasColumnType("varchar")
                .IsUnicode(false);
            builder.Property(t => t.LogoUrl)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsFixedLength(false)
                .HasColumnName("LogoUrl")
                .HasColumnType("varchar")
                .IsUnicode(false);
            builder.Property(t => t.Initials)
                .IsRequired(true)
                .HasMaxLength(3)
                .IsFixedLength(true)
                .HasColumnName("Initials")
                .HasColumnType("char")
                .IsUnicode(false);
            builder.Property(t => t.Budget)
                .IsRequired(true)
                .HasColumnName("Budget")
                .HasColumnType("money");
            builder.Property(t => t.PrimaryKitColorId)
                .IsRequired(true)
                .HasColumnName("PrimaryKitColorId")
                .HasColumnType("int");
            builder.Property(t => t.SecondaryKitColorId)
                .IsRequired(true)
                .HasColumnName("SecondaryKitColorId")
                .HasColumnType("int");
            builder.Property(t => t.TownId)
                 .IsRequired(true)
                 .HasColumnName("TownId")
                 .HasColumnType("int");

            builder
                .HasMany(t => t.HomeGames)
                .WithOne(t => t.HomeTeam)
                .HasForeignKey(t => t.HomeTeamId);
            builder
                .HasMany(t => t.AwayGames)
                .WithOne(t => t.AwayTeam)
                .HasForeignKey(t => t.AwayTeamId);
            builder
                .HasMany(t => t.Players)
                .WithOne(t => t.Team)
                .HasForeignKey(t => t.TeamId);
            builder
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(t => t.PrimaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(t => t.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(t => t.Town)
                .WithMany(t => t.Teams)
                .HasForeignKey(t => t.TownId);
        }
    }
}
