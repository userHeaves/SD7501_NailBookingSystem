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
        [DisplayName("Add On Name")]
        public string Name { get; set; }
        [Range(1, 100)]
        [DisplayName("Add On price")]
        public double BookingOrder { get; set; }

    }
}
