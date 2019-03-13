using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.Username)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsFixedLength(false)
                .HasColumnName("Username")
                .HasColumnType("varchar")
                .IsUnicode(false);
            builder.Property(u => u.Password)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsFixedLength(false)
                .HasColumnName("Password")
                .HasColumnType("varchar")
                .IsUnicode(false);
            builder.Property(u => u.Email)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsFixedLength(false)
                .HasColumnName("Email")
                .HasColumnType("varchar")
                .IsUnicode(false);
            builder.Property(u => u.Name)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsFixedLength(false)
                .HasColumnName("Name")
                .HasColumnType("varchar")
                .IsUnicode(false);
            builder.Property(u => u.Balance)
                .IsRequired(true)
                .HasColumnName("Balance")
                .HasColumnType("money");

            //builder
            //    .HasMany(u => u.Bets)
            //    .WithOne(u => u.User)
            //    .HasForeignKey(u => u.BetId);
        }
    }
}
