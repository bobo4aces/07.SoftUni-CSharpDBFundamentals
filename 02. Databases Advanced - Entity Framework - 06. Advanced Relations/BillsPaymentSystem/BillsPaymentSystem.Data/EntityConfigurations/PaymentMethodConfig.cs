using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillsPaymentSystem.Data.EntityConfigurations
{
    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Type)
                .IsRequired(true)
                .HasMaxLength(11)
                .HasColumnName("Type")
                .HasColumnType("varchar(11)")
                .IsUnicode(false);
            builder.Property(p => p.UserId)
                .IsRequired(true)
                .HasColumnName("UserId")
                .HasColumnType("int");
            builder.Property(p=>p.BankAccountId)
                .IsRequired(false)
                .HasColumnName("BankAccountId")
                .HasColumnType("int");
            builder.Property(p => p.CreditCardId)
                .IsRequired(false)
                .HasColumnName("CreditCardId")
                .HasColumnType("int");

            builder
                .HasOne(p => p.CreditCard)
                .WithOne(p => p.PaymentMethod);
            builder
                .HasOne(p => p.BankAccount)
                .WithOne(p => p.PaymentMethod);
            builder
                .HasOne(p => p.User)
                .WithMany(p => p.PaymentMethods)
                .HasForeignKey(p => p.UserId);
        }
    }
}
