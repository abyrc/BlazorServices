using Blazor.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        private readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");  // el nombre correcto de tu tabla en DB
        }
    }
}
