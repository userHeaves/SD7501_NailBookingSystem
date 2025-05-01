using Microsoft.AspNetCore.Mvc;
using SD7501_NailBookingSystem.Data;

namespace SD7501_NailBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookingController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            var objBookingList = _db.Bookings.ToList();
            return View(objBookingList);
        }
    }
}
