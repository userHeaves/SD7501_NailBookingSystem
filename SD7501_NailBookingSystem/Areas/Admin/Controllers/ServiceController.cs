using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SD7501_NailBookingSystem.Data;
using SD7501_NailBookingSystem.Models;

namespace SD7501_NailBookingSystem.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ServiceController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Service> objServiceList = _db.Services.ToList(); 
            return View(objServiceList);
        }
        public class AddOnController : Controller
        {
            private readonly ApplicationDbContext _db;
            public AddOnController(ApplicationDbContext db)
            {
                _db = db;
            }


        }
            public IActionResult Select()
            {
                List<AddOn> objAddOnList = _db.AddOns.ToList();
                return View();
            }
    }
}
