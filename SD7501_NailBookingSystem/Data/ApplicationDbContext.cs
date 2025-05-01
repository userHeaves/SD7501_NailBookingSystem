using Microsoft.EntityFrameworkCore;
using SD7501_NailBookingSystem.Models;

namespace SD7501_NailBookingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().HasData(
                new Booking { Id = 1, Name = "Alice", BookingOrder = 1 },
                new Booking { Id = 2, Name = "Jasmine", BookingOrder = 2 },
                new Booking { Id = 3, Name = "Kelly", BookingOrder = 3 }
                );
        }
        public DbSet<Service> Services { get; set; }
    }
}
