using CarRentalStudio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace CarRentalStudio.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Rental>()
             .HasOne(r => r.Car)
             .WithMany(c => c.Rentals)
             .HasForeignKey(r => r.CarId);

           /* modelBuilder.Entity<Car>()
                .Property(c => c.DailyRate)
                .HasPrecision(18, 2); // Equivalent to decimal(18, 2)

            modelBuilder.Entity<Rental>()
                .Property(r => r.Price)
                .HasPrecision(18, 2);*/

        }
    }
}
