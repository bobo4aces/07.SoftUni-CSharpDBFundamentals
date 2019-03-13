using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P03_SalesDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P03_SalesDatabase.Data.EntityConfiguration
{
    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(s => s.StoreId);
            builder.Property(s => s.Name)
                .IsRequired(true)
                .HasMaxLength(80)
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .IsUnicode(true);
        }
    }
}
