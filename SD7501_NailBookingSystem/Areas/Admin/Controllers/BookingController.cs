using Microsoft.AspNetCore.Mvc;
using SD7501_NailBookingSystem.DataAccess.Repository.IRepository;
using SD7501_NailBookingSystem.Data;
using SD7501_NailBookingSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using SD7501_NailBookingSystem.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using SD7501_NailBookingSystem.Utilities;

namespace SD7501_NailBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class BookingController : Controller
    {
        private readonly IUnityOfWork _unitOfWork;

        //Main View
        public BookingController(IUnityOfWork unityOfWork)
        {
            _unitOfWork = unityOfWork;

        }
        public IActionResult Index()
        {
            var objBookingList = _unitOfWork.Booking.GetAll().ToList();
            return View(objBookingList);
        }

        ////Upsert
        //public IActionResult Upsert(int? id)
        //{
        //    ServiceVM serviceVM = new()
        //    {
        //        ServiceList = _unitOfWork.Booking.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.Name,
        //            Value = u.Id.ToString()
        //        }),
        //        Service = new Service()
        //    };

        //    if (id == null || id == 0)
        //    {
        //        // Create
        //        return View(serviceVM);
        //    }
        //    else
        //    {
        //        // Update
        //        serviceVM.Service = _unitOfWork.Service.Get(u => u.Id == id);
        //        return View(serviceVM);
        //    }
        //}


        //[HttpPost] 
        //public IActionResult Upsert(ServiceVM serviceVM, IFormFile? file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string wwwRootPath = _webHostEnvironment.WebRootPath;
        //        if (file != null)
        //        {
        //            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); // Generates a unique file name
        //            string productPath = Path.Combine(wwwRootPath, @"images\product");

        //            if (!string.IsNullOrEmpty(serviceVM.Service.ImageUrl))
        //            {
        //                //delete the old image by getting the path of that image
        //                var oldImagePath = Path.Combine(wwwRootPath, serviceVM.Service.ImageUrl.Trim('\\'));

        //                if (System.IO.File.Exists(oldImagePath))
        //                {
        //                    System.IO.File.Delete(oldImagePath);
        //                }

        //            }

        //            using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
        //            {
        //                file.CopyTo(fileStream);
        //            }

        //            serviceVM.Service.ImageUrl = @"\images\product\" + fileName;
        //        }

        //        if (serviceVM.Service.Id == 0)
        //        {
        //            _unitOfWork.Service.Add(serviceVM.Service);
        //        }
        //        else
        //        {
        //            _unitOfWork.Service.Update(serviceVM.Service);

        //        }
        //        _unitOfWork.Save();
        //        TempData["success"] = "Service created successfully";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        serviceVM.BookingList = _unitOfWork.Booking.GetAll().Select(u => new SelectListItem
        //        {
        //            Text = u.Name,
        //            Value = u.Id.ToString()
        //        });

        //        return View(serviceVM);
        //    }
        //}

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
                _unitOfWork.Booking.Add(bookingobj);
                _unitOfWork.Save();
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
            Booking? bookingFromDB = _unitOfWork.Booking.Get(u => u.Id == id);

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
                _unitOfWork.Booking.Update(bookingobj);
                _unitOfWork.Save();
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
            Booking? bookingFromDB = _unitOfWork.Booking.Get(u => u.Id == id);

            if (bookingFromDB == null)
            {
                return NotFound();
            }
            return View(bookingFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Booking? bookingobj = _unitOfWork.Booking.Get(u => u.Id == id);
            if (bookingobj == null)
            {
                return NotFound();

            }
            _unitOfWork.Booking.Remove(bookingobj);
            _unitOfWork.Save();
            TempData["success"] = "Booking deleted successful";
            return RedirectToAction("Index");
        }

    }
}
