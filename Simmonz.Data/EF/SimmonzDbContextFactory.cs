using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace Simmonz.Data.EF
{
    public class SimmonzDbContextFactory : IDesignTimeDbContextFactory<SimmonzDbContext>
    {
        public SimmonzDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("SimmonzDatabase");
            var optionsBuilder = new DbContextOptionsBuilder<SimmonzDbContext>();
            optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;database=simmonz;user=root;password=matkhaula");
            return new SimmonzDbContext(optionsBuilder.Options);
        }
    } 
}
