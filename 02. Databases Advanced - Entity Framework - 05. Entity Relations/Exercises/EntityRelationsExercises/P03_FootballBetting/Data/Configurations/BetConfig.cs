using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class BetConfig : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(b => b.BetId);
            builder.Property(b => b.Amount)
                .IsRequired(true)
                .HasColumnName("Amount")
                .HasColumnType("money");
            builder.Property(b => b.Prediction)
                .IsRequired(true)
                .HasMaxLength(1)
                .IsFixedLength(true)
                .HasColumnName("Prediction")
                .HasColumnType("char")
                .IsUnicode(false);
            builder.Property(b => b.DateTime)
                .IsRequired(true)
                .HasColumnName("DateTime")
                .HasColumnType("date");
            builder.Property(b => b.UserId)
                .IsRequired(true)
                .HasColumnName("UserId")
                .HasColumnType("int");
            builder.Property(b => b.GameId)
                .IsRequired(true)
                .HasColumnName("GameId")
                .HasColumnType("int");

            builder
                .HasOne(b => b.Game)
                .WithMany(b => b.Bets)
                .HasForeignKey(b => b.GameId);
            builder
                .HasOne(b => b.User)
                .WithMany(b => b.Bets)
                .HasForeignKey(b => b.UserId);
                
        }
    }
}
