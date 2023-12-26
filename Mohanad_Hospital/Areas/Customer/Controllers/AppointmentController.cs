using Hospital.DataAccess.Repository.IRepository;
using Hospital.Models;
using Hospital.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Mohanad_Hospital.Areas.Customer.Controllers
{
    [Area("customer")]
    [Authorize]
    public class AppointmentController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public AppointmentVM AppointmentVM { get; set; }
        public AppointmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            AppointmentVM = new()
            {
                AppointmentList = _unitOfWork.Appointment.GetAll(u => u.ApllicationUserID == userId,
                includeProperties: "Doctor")
            };


            

            return View(AppointmentVM);
        }
        public IActionResult AppointmentSummary()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			AppointmentVM = new()
			{
				AppointmentList = _unitOfWork.Appointment.GetAll(u => u.ApllicationUserID == userId,
				includeProperties: "Doctor"),
                AppointmentHeader = new() 
			};
            AppointmentVM.AppointmentHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u=>u.Id == userId);

            AppointmentVM.AppointmentHeader.Name = AppointmentVM.AppointmentHeader.ApplicationUser.Name;
            AppointmentVM.AppointmentHeader.City = AppointmentVM.AppointmentHeader.ApplicationUser.City;
            AppointmentVM.AppointmentHeader.Region = AppointmentVM.AppointmentHeader.ApplicationUser.Region;
            AppointmentVM.AppointmentHeader.Number = AppointmentVM.AppointmentHeader.ApplicationUser.Number;



            return View(AppointmentVM);
        }
    }
}
