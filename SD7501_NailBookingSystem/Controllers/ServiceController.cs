using Microsoft.AspNetCore.Mvc;

namespace SD7501_NailBookingSystem.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
