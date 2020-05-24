using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simmonz.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.Data.Configurations
{
    public class ShippingFeeConfiguration : IEntityTypeConfiguration<ShippingFee>
    {
        public void Configure(EntityTypeBuilder<ShippingFee> builder)
        {
            builder.ToTable("ShippingFees");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.District).IsRequired();

            builder.Property(x => x.Fee).IsRequired();

        }
    }
}
