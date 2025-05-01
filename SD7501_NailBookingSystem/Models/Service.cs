using System.ComponentModel.DataAnnotations;

namespace SD7501_NailBookingSystem.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        public decimal Cost { get; set; }
    }
}
