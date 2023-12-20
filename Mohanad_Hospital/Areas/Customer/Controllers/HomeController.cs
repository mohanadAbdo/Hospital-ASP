using Microsoft.AspNetCore.Mvc;
using Hospital.Models;
using System.Diagnostics;
using Hospital.DataAccess.Repository.IRepository;

namespace Mohanad_Hospital.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Doctor> doctorList = _unitOfWork.Doctor.GetAll(includeProperties: "Category");
            return View(doctorList);
        }
        public IActionResult Details(int doctorId)
        {
            Doctor doctor = _unitOfWork.Doctor.Get(u=>u.Id== doctorId, includeProperties: "Category");
            return View(doctor);
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
