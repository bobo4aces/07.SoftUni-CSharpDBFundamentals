using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillsPaymentSystem.Data.EntityConfigurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.FirstName)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("FirstName")
                .HasColumnType("nvarchar(50)")
                .IsUnicode(true);
            builder.Property(u => u.LastName)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("LastName")
                .HasColumnType("nvarchar(50)")
                .IsUnicode(true);
            builder.Property(u => u.Email)
                .IsRequired(true)
                .HasMaxLength(80)
                .HasColumnName("Email")
                .HasColumnType("varchar(80)")
                .IsUnicode(false);
            builder.Property(u => u.Password)
               .IsRequired(true)
               .HasMaxLength(25)
               .HasColumnName("Password")
               .HasColumnType("varchar(25)")
               .IsUnicode(false);

            builder
                .HasMany(u => u.PaymentMethods)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);
        }
    }
}
