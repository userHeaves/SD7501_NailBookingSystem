using Microsoft.AspNetCore.Mvc;
using SD7501_NailBookingSystem.DataAccess.Repository.IRepository;
using SD7501_NailBookingSystem.Data;
using SD7501_NailBookingSystem.Models;

namespace SD7501_NailBookingSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingRepository _bookingRepo;

        //Main View
        public BookingController(IBookingRepository db)
        {
            _bookingRepo = db;

        }
        public IActionResult Index()
        {
            var objBookingList = _bookingRepo.GetAll();
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
                _bookingRepo.Add(bookingobj);
                _bookingRepo.Save();
                TempData["success"] = "Booking created successfully";
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Booking creation failed";
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
            Booking? bookingFromDB = _bookingRepo.Get(u => u.Id == id);

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
                _bookingRepo.Update(bookingobj);
                _bookingRepo.Save();
                TempData["success"] = "Booking updated successfully";
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                TempData["error"] = "Edit failed";
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
            Booking? bookingFromDB = _bookingRepo.Get(u => u.Id == id);

            if (bookingFromDB == null)
            {
                return NotFound();
            }
            return View(bookingFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Booking? bookingobj = _bookingRepo.Get(u => u.Id == id);
            if (bookingobj == null)
            {
                return NotFound();

            }
            _bookingRepo.Remove(bookingobj);
            _bookingRepo.Save();
            TempData["success"] = "Booking deleted successful";
            return RedirectToAction("Index");
        }

    }
}
