using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD7501_NailBookingSystem.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [Required]
        public int OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        [ValidateNever]
        public Service Service { get; set; }

        public int Count { get; set; }
        public double Price { get; set; }
    }
}
