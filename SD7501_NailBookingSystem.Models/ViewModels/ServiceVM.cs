using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD7501_NailBookingSystem.Models.ViewModels
{
    public class ServiceVM
    {
        public Service Service { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> BookingList { get; set; }
    }
}
