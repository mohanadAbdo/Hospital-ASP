using Microsoft.AspNetCore.Mvc;
using Hospital.Models;
using System.Diagnostics;
using Hospital.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Localization;
using WebProje.Services;

namespace Mohanad_Hospital.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, LanguageService localization)
        {

            _logger = logger;
            _unitOfWork = unitOfWork;
            _localization = localization;
        }

        public IActionResult ChangeLanguage(string culture)
        {
            // Set the culture cookie
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                }
            );

            // Redirect to the current URL to apply the new culture
            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult Index()
        {
            ViewBag.Welcom = _localization.Getkey("hi").Value;
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;
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
