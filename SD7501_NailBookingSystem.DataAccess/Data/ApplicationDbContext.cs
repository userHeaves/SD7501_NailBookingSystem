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
        public DbSet<Service> Services { get; set; }
        public DbSet<AddOn> AddOns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Not sure if we need this:
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>().HasData(
                new Booking { Id = 1, Name = "Alice", BookingOrder = 1 },
                new Booking { Id = 2, Name = "Jasmine", BookingOrder = 2 },
                new Booking { Id = 3, Name = "Kelly", BookingOrder = 3 }
                );
            modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, Type = "Manicure Gel", Cost = 40 },
                new Service { Id = 2, Type = "Pedicure Gel", Cost = 40 },
                new Service { Id = 3, Type = "SNS", Cost = 55 },
                new Service { Id = 4, Type = "Acrylic", Cost = 60 },
                new Service { Id = 5, Type = "BIAB", Cost = 50 }
                );
            modelBuilder.Entity<AddOn>().HasData(
                new AddOn { Id = 1, Type = "French Tip", Cost = 8},
                new AddOn { Id = 2, Type = "Nail Art", Cost = 8},
                new AddOn { Id = 3, Type = "Sticker and Gems", Cost = 8}
                );
        }
    }
}
