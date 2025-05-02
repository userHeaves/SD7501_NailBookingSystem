using Microsoft.AspNetCore.Mvc;
using SD7501_NailBookingSystem.Data;
using SD7501_NailBookingSystem.Models;

namespace SD7501_NailBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;

        //Main View
        public BookingController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            var objBookingList = _db.Bookings.ToList();
            return View(objBookingList);
        }


        //Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Booking bookingobj)
        {
            if (bookingobj.Name == bookingobj.BookingOrder.ToString())
            {
                ModelState.AddModelError("Name", "This Display Order cannot exactly match with the name");

            }
            if (ModelState.IsValid)
            {
                _db.Bookings.Add(bookingobj);
                _db.SaveChanges();
                TempData["success"] = "Catergory created successfully";
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Creation failed";
            }
            return View();
        }

        //Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Find the id
            Booking? bookingFromDB = _db.Bookings.Find(id);

            if (bookingFromDB == null)
            {
                return NotFound();
            }
            return View(bookingFromDB);
        }

        [HttpPost]
        public IActionResult Edit(Booking bookingobj)
        {
            if (ModelState.IsValid)
            {
                _db.Bookings.Update(bookingobj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Find the id
            Booking? bookingFromDB = _db.Bookings.Find(id);

            if (bookingFromDB == null)
            {
                return NotFound();
            }
            return View(bookingFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Booking? bookingobj = _db.Bookings.Find(id);
            if (bookingobj == null)
            {
                return NotFound();

            }
            _db.Bookings.Remove(bookingobj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
