using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SD7501_NailBookingSystem.Models;


namespace SD7501_NailBookingSystem.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }

        //-------------Description-----------------//

        public string Description { get; set; }

        [Required]
        [Range (1, 1000)]
        public decimal Cost { get; set; }

        //-------------------------------------//
        //Foreign Key
        public int BookingId { get; set; }

        // Navigation Property
        [ForeignKey("BookingId")]
        [ValidateNever]
        public Booking Booking { get; set; }

        //-------------------------------------//
        [ValidateNever]
        public string ImageUrl { get; set; }
    }
}
