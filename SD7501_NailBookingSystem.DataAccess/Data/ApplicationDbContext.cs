using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SD7501_NailBookingSystem.Models;

namespace SD7501_NailBookingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Service> Services { get; set; }
        //public DbSet<AddOn> AddOns { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Not sure if we need this:
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>().HasData(
                new Booking { Id = 1, Name = "French Tip", BookingOrder = 8 },
                new Booking { Id = 2, Name = "Nail Art", BookingOrder = 8 },
                new Booking { Id = 3, Name = "Sticker and Gems", BookingOrder = 8 }
                );
            modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, Type = "Manicure Gel", Cost = 40,
                    Description = "Our Manicure Gel starts with shaping your nails and applying a base coat, followed by layers of gel polish cured under a UV lamp. It finishes with a glossy top coat, leaving you with beautiful, chip-free nails that last for weeks!", 
                    BookingId = 1, 
                    ImageUrl = "ManicureGel.jpg"},
                new Service { Id = 2, Type = "Pedicure Gel", Cost = 40,
                    Description = "Our Pedicure Gel includes a relaxing foot soak, nail shaping, and cuticle care, followed by gel polish cured under a UV lamp. The result is smooth, vibrant toes with a long-lasting, high-shine finish!", 
                    BookingId = 1, 
                    ImageUrl = "PedicureGel.jpg"},
                new Service { Id = 3, Type = "SNS", Cost = 55,
                    Description = "Our SNS treatment begins with shaping your nails and applying a bonding base, then dipping them into vitamin-rich powder for strength and color. It finishes with a glossy seal—no UV light needed—for a lightweight, durable look.",
                    BookingId = 2, 
                    ImageUrl = "SNS.jpg"},
                new Service {Id = 4, Type = "Acrylic", Cost = 60,
                    Description = "Our Acrylic service starts by applying a liquid and powder mix to your natural nails or extensions, creating a strong, sculpted shape. It's then buffed and polished for a durable, long-lasting finish perfect for any style.", 
                    BookingId = 2, 
                    ImageUrl = "Acrylic.jpg"},
                new Service {Id = 5, Type = "BIAB", Cost = 50,
                    Description = "BIAB (Builder in a Bottle) is a strengthening gel applied like polish but acts like acrylic, perfect for growing and protecting natural nails. It's finished with a glossy top coat for a natural, long-lasting look.", 
                    BookingId = 3, 
                    ImageUrl = "BIAB.jpg"}
                );
            /*modelBuilder.Entity<AddOn>().HasData(
                new AddOn { Id = 1, Type = "French Tip", Cost = 8},
                new AddOn { Id = 2, Type = "Nail Art", Cost = 8},
                new AddOn { Id = 3, Type = "Sticker and Gems", Cost = 8}
                );*/
        }
    }
}
