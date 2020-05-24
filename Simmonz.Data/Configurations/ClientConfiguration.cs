using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simmonz.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.Data.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.Email).IsRequired();

            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.Address).IsRequired();
           
        }
    }
}
