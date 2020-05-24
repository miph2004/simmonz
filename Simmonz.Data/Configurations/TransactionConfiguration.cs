using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simmonz.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.Data.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.PaymentMethod).IsRequired();

            builder.Property(x => x.AddressNumber).IsRequired();

            builder.Property(x => x.AddressDistrict).IsRequired();
            builder.Property(x => x.AddressStreet).IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(0);
           
        }
    }
}
