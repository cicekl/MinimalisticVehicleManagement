using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.Service.Models;

namespace Project.Service.DataAccess
{
    public class VehicleDBContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public VehicleDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<VehicleMake> VehicleMake { get; set; }
        public DbSet<VehicleModel> VehicleModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<VehicleMake>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
            });


            // Configure your model relationships, constraints, etc. here
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("VehicleDBConnection"));
        }
    }
}
