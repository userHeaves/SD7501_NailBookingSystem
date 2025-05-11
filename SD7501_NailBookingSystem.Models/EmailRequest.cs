using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD7501_NailBookingSystem.Models
{
    public class EmailRequest
    {
        [Required]
        public string EmailTo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Enquiry { get; set; }
        [Required]
        public string Description { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
