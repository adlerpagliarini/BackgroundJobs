using BackgroundJobs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BackgroundJobs.Data
{
    public class ModelFlowContext : DbContext
    {
        public DbSet<Model> ModelFlow { get; set; }
        public DbSet<Node> NodeFlow { get; set; }
        public static IConfiguration ConnectionConfiguration
            => new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ModelMap());
            modelBuilder.ApplyConfiguration(new NodeMap());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionConfiguration.GetConnectionString("DefaultConnection"));
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
