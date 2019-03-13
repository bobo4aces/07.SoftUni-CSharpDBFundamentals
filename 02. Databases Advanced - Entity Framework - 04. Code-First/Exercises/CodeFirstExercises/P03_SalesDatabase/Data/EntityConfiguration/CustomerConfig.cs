using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_SalesDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_SalesDatabase.Data.EntityConfiguration
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.CustomerId);
            builder.Property(c => c.Name)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .IsUnicode(true);
            builder.Property(c => c.Email)
                .IsRequired(true)
                .HasMaxLength(80)
                .HasColumnName("Email")
                .HasColumnType("varchar")
                .IsUnicode(false);
            builder.Property(c => c.CreditCardNumber)
                .IsRequired(true)
                .HasMaxLength(int.MaxValue)
                .HasColumnName("CreditCardNumber")
                .HasColumnType("varchar");
        }
    }
}
