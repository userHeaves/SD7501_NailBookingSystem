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
        private readonly IUnityOfWork _unitOfWork;//Updated syntax for UOF
        public ServiceController(IUnityOfWork unityOfWork)//Updated syntax for UOF
        {
            _unitOfWork = unityOfWork;//Updated syntax for UOF
        }

        public IActionResult Index()
        {
            List<Service> objServiceList = _unitOfWork.Service.GetAll().ToList(); //Updated syntax for UOF
            return View(objServiceList);
        }
        public class AddOnController : Controller
        {
            private readonly IUnityOfWork _unitOfWork;//Updated syntax for UOF
            public AddOnController(IUnityOfWork unityOfWork)//Updated syntax for UOF
            {
                _unitOfWork = unityOfWork; //Updated syntax for UOF
            }


        }
            public IActionResult Select()
            {
                List<AddOn> objAddOnList = _unitOfWork.AddOn.GetAll().ToList(); //Updated syntax for UOF
                return View();
            }
    }
}
