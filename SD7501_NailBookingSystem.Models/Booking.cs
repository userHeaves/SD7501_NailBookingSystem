using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SD7501_NailBookingSystem.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Booking Name")]
        public string Name { get; set; }
        [Range(1, 100)]
        [DisplayName("Booking Order")]
        public int BookingOrder { get; set; }

    }
}
