using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillsPaymentSystem.Data.EntityConfigurations
{
    public class CreditCardConfig : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasKey(c => c.CreditCardId);
            builder.Property(c => c.Limit)
                .IsRequired(true)
                .HasColumnName("Limit")
                .HasColumnType("money");
            builder.Property(c => c.MoneyOwned)
                .IsRequired(true)
                .HasColumnName("MoneyOwned")
                .HasColumnType("money");
            builder.Property(c => c.ExpirationDate)
                .IsRequired(true)
                .HasColumnName("ExpirationDate")
                .HasColumnType("date");

            builder
                .HasOne(c => c.PaymentMethod)
                .WithOne(c => c.CreditCard);
        }
    }
}
