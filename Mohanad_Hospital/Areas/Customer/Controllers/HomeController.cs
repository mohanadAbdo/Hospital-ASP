using Microsoft.AspNetCore.Mvc;
using Hospital.Models;
using System.Diagnostics;
using Hospital.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
            Appointment appointment = new()
            {
                Doctor = _unitOfWork.Doctor.Get(u => u.Id == doctorId, includeProperties: "Category"),

                DoctorId = doctorId

            };

            return View(appointment);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(Appointment appointment)
        {
         
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            appointment.ApllicationUserID = userId;
            Appointment appoinmentFromDb = _unitOfWork.Appointment.Get(u=>u.ApllicationUserID == userId&&
            u.DoctorId == appointment.DoctorId);
            if(appoinmentFromDb != null) 
            {
                _unitOfWork.Appointment.Update(appoinmentFromDb);

            }
            else
            {
                _unitOfWork.Appointment.Add(appointment);
            }

            
            _unitOfWork.Save(); 
           

            return RedirectToAction(nameof(Index));
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
