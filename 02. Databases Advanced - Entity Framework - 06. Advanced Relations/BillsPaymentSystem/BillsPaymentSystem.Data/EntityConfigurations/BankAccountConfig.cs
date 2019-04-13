using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillsPaymentSystem.Data.EntityConfigurations
{
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(b => b.BankAccountId);
            builder.Property(b => b.Balance)
                .IsRequired(true)
                .HasColumnName("Balance")
                .HasColumnType("money");
            builder.Property(u => u.BankName)
                .IsRequired(true)
                .HasMaxLength(50)
                .HasColumnName("BankName")
                .HasColumnType("nvarchar(50)")
                .IsUnicode(true);
            builder.Property(u => u.SwiftCode)
                .IsRequired(true)
                .HasMaxLength(20)
                .HasColumnName("SwiftCode")
                .HasColumnType("varchar(20)")
                .IsUnicode(false);

            builder
                .HasOne(b => b.PaymentMethod)
                .WithOne(b => b.BankAccount);
        }
    }
}
