using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SD7501_NailBookingSystem.DataAccess.Repository.IRepository;
using SD7501_NailBookingSystem.Models;
using SD7501_NailBookingSystem.Utilities;

namespace SD7501_NailBookingSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Service> serviceList = _unitOfWork.Service.GetAll(includeProperties: "Booking");
            return View(serviceList);
        }

        public IActionResult Details(int serviceId)
        {
            ShoppingCart cart = new()
            {
                Service = _unitOfWork.Service.Get(u => u.Id == serviceId, includeProperties: "Booking"),
                Count = 1,
                ServiceId = serviceId,
                AddOns = false,
                AddOnPrice = 0
            };
            return View(cart);

            //Service service = _unitOfWork.Service.Get(u => u.Id == serviceId, includeProperties: "Booking");
            //return View(service);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId &&
                                                                         u.ServiceId == shoppingCart.ServiceId);
            // Enforce 1 count per service type
            if (shoppingCart.Count < 1)
            {
                shoppingCart.Count = 1;
            }

            if (cartFromDb != null)
            {

                if (shoppingCart.AddOns == true)
                {
                    //When AddOns Feature is True
                    cartFromDb.AddOns = shoppingCart.AddOns;
                    cartFromDb.Count = shoppingCart.Count;
                    _unitOfWork.ShoppingCart.Update(cartFromDb);
                    _unitOfWork.Save();
                }

                else
                {
                    //When AddOns Feature is False
                    cartFromDb.Count = shoppingCart.Count;
                    _unitOfWork.ShoppingCart.Update(cartFromDb);
                    _unitOfWork.Save();

                }
            }
            else 
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();

                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }

            TempData["success"] = "Cart updated successfully";
            return RedirectToAction(nameof(Index));
        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
