using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SD7501_NailBookingSystem.Data;
using SD7501_NailBookingSystem.DataAccess.Repository.IRepository;
using SD7501_NailBookingSystem.Models;

namespace SD7501_NailBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")] // Updated syntax for Area
    public class ServiceController : Controller
    {
        private readonly IUnityOfWork _unitOfWork;
        public ServiceController(IUnityOfWork unityOfWork)
        {
            _unitOfWork = unityOfWork;
        }

        public IActionResult Index()
        {
            List<Service> objServiceList = _unitOfWork.Service.GetAll().ToList();
            return View(objServiceList);
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Service serviceobj)
        {
            if (serviceobj.Type == serviceobj.Cost.ToString())
            {
                ModelState.AddModelError("Name", "This Display Order cannot exactly match with the name");

            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Service.Add(serviceobj);
                _unitOfWork.Save();
                TempData["success"] = "Service created successfully";
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Service creation failed";
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
            Service? serviceFromDB = _unitOfWork.Service.Get(u => u.Id == id);

            if (serviceFromDB == null)
            {
                return NotFound();
            }
            return View(serviceFromDB);
        }

        [HttpPost]
        public IActionResult Edit(Service serviceobj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Service.Update(serviceobj);
                _unitOfWork.Save();
                TempData["success"] = "Service updated successfully";
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
            Service? serviceFromDB = _unitOfWork.Service.Get(u => u.Id == id);

            if (serviceFromDB == null)
            {
                return NotFound();
            }
            return View(serviceFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Service? serviceobj = _unitOfWork.Service.Get(u => u.Id == id);
            if (serviceobj == null)
            {
                return NotFound();

            }
            _unitOfWork.Service.Remove(serviceobj);
            _unitOfWork.Save();
            TempData["success"] = "Service deleted successful";
            return RedirectToAction("Index");
        }

        public class AddOnController : Controller
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
        }
    }

}
