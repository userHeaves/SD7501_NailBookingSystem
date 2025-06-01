using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using SD7501_NailBookingSystem.Data;
using SD7501_NailBookingSystem.DataAccess.Repository;
using SD7501_NailBookingSystem.DataAccess.Repository.IRepository;
using SD7501_NailBookingSystem.Models;
using SD7501_NailBookingSystem.Models.ViewModels;
using SD7501_NailBookingSystem.Utilities;

namespace SD7501_NailBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")] // Updated syntax for Area
    [Authorize(Roles = SD.Role_Admin)]
    public class ServiceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ServiceController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            var objServiceList = _unitOfWork.Service.GetAll(includeProperties:"Booking").ToList();
            return View(objServiceList);
        }

        public IActionResult Upsert(int? id)
        {
            ServiceVM serviceVM = new()
            {
                BookingList = _unitOfWork.Booking.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Service = new Service()
            };

            if (id == null || id == 0)
            {
                // Create
                return View(serviceVM);
            }
            else
            {
                // Update
                serviceVM.Service = _unitOfWork.Service.Get(u => u.Id == id);
                return View(serviceVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ServiceVM serviceVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); // Generates a unique file name
                    string servicePath = Path.Combine(wwwRootPath, @"images\service");

                    if (!string.IsNullOrEmpty(serviceVM.Service.ImageUrl))
                    {
                        //delete the old image by getting the path of that image
                        var oldImagePath = Path.Combine(wwwRootPath, serviceVM.Service.ImageUrl.Trim('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }

                    using (var fileStream = new FileStream(Path.Combine(servicePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    serviceVM.Service.ImageUrl = @"\images\service\" + fileName;
                }

                if (serviceVM.Service.Id == 0)
                {
                    _unitOfWork.Service.Add(serviceVM.Service);
                }
                else
                {
                    _unitOfWork.Service.Update(serviceVM.Service);

                }
                _unitOfWork.Save();
                TempData["success"] = "Service created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                serviceVM.BookingList = _unitOfWork.Booking.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

                return View(serviceVM);
            }

        }

        [HttpPost]
        public IActionResult Edit(Service serviceobj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Service.Update(serviceobj);
                _unitOfWork.Save();
                TempData["success"] = "Service update successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        //-------------------------------API END POINT-----------------------------------------------------------------//
        #region API calls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Service> objServiceList = _unitOfWork.Service.GetAll(includeProperties: "Booking").ToList();
            return Json(new { data = objServiceList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var serviceToBeDeleted = _unitOfWork.Service.Get(u => u.Id == id);
            if (serviceToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, serviceToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Service.Remove(serviceToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
        //-------------------------------API END POINT-----------------------------------------------------------------//
        /*public class AddOnController : Controller
        {
            private readonly IUnityOfWork _unitOfWork;
            public AddOnController(IUnityOfWork unityOfWork)
            {
                _unitOfWork = unityOfWork;
            }


        }
        public IActionResult Select()
        {
            List<AddOn> objAddOnList = _unitOfWork.AddOn.GetAll().ToList();
            return View();
        }*/
    }

}
