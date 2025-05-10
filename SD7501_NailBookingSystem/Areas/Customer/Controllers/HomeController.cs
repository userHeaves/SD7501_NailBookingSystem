using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SD7501_NailBookingSystem.DataAccess.Repository.IRepository;
using SD7501_NailBookingSystem.Models;

namespace SD7501_NailBookingSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnityOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnityOfWork unitOfWork)
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
            Service service = _unitOfWork.Service.Get(u => u.Id == serviceId, includeProperties: "Booking");
            return View(service);
        }

        public IActionResult Privacy()
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
