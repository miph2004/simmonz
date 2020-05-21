using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simmonz.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.Data.Configurations
{
    public class ProductConfigConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ProductName).IsRequired();

            builder.Property(x => x.CategoryId).IsRequired();

            builder.Property(x => x.Quantity).IsRequired().HasDefaultValue(0);
        }
    }
}
