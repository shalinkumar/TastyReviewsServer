using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<RestaurantModel> Restaurants { get; set; }
        public DbSet<RestaurantImages> RestaurantImages { get; set; }

        public DbSet<Bookings> Bookings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

           // builder.Entity<RestaurantModel>()
           //.HasKey(a => a.Guid);

           // builder.Entity<RestaurantImages>()
           // .HasOne(b => b.RestaurantModel)
           // .WithMany(a => a.Images) // Navigation property in Author class
           // .HasForeignKey(b => b.Guid);
            // builder.Entity<RestaurantModel>()
            //.HasMany(e => e.Images)
            //.WithOne(e => e.RestaurantModel)
            //.HasForeignKey(e => e.Guid)
            //.HasPrincipalKey(e => e.Id);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
