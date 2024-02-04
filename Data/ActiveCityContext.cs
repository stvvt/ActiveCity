using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ActiveCity.Models;

namespace ActiveCity.Data
{
    public class ActiveCityContext : DbContext
    {
        public ActiveCityContext (DbContextOptions<ActiveCityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().HasData(new User { Id = 1, Username = "admin", Password = "admin", FirstName = "System", LastName = "Admin", Role = Role.Admin });
        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<Citizen> Citizen { get; set; } = default!;
    }
}
