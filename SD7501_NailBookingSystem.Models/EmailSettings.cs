using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD7501_NailBookingSystem.Models
{
    public class EmailSettings
    {
        public string EmailAddress { get; set; }
        public string CompanyName { get; set; }
        public string Password { get; set; }
        public string ServerHost { get; set; }
        public int ServerPort { get; set; }
    }
}
