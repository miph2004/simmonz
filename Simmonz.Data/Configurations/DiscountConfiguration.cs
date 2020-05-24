using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simmonz.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.Data.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discounts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.DiscountPercent).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.Status).HasDefaultValue(0).IsRequired();
        }
    }
}
