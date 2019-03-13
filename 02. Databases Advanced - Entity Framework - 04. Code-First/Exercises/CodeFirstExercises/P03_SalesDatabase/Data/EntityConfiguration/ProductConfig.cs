using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data.EntityConfiguration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);
            builder.Property(p => p.Name)
                .IsRequired(true)
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .HasMaxLength(50)
                .IsUnicode(true);
            builder.Property(p => p.Quantity)
                .IsRequired(true)
                .HasColumnName("Quantity")
                .HasColumnType("decimal(15,2)");
            builder.Property(p=>p.Price)
                .IsRequired(true)
                .HasColumnName("Price")
                .HasColumnType("money");
            builder.Property(p => p.Description)
                .IsRequired(false)
                .HasColumnName("Description")
                .HasMaxLength(250)
                .HasColumnType("nvarchar")
                .HasDefaultValue("No description");
        }
    }
}
