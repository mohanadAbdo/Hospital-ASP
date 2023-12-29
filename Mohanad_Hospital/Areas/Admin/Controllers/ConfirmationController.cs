using Hospital.DataAccess.Repository.IRepository;
using Hospital.Models;
using Hospital.Models.ViewModels;
using Hospital.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Mohanad_Hospital.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class ConfirmationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        [BindProperty]
        public ConfirmationVM ConfirmationVM { get; set; }
        public ConfirmationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
 


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int confirmId)
        {
            ConfirmationVM = new ()
            {
                AppointmentHeader = _unitOfWork.AppointmentHeader.Get(u => u.Id == confirmId, includeProperties: "ApplicationUser"),
                AppointmentDetail = _unitOfWork.AppointmentDetail.GetAll(u => u.AppointmentHeaderId == confirmId, includeProperties: "Doctor")
            };

            return View(ConfirmationVM);
        }

        [HttpPost]
        [Authorize(Roles =SD.Admin_Role)]
        public IActionResult UpdateConfirmationDetail(int confirmId)
        {
            var appointmentFromDb = _unitOfWork.AppointmentHeader.Get(u => u.Id == ConfirmationVM.AppointmentHeader.Id);
            appointmentFromDb.TheName = ConfirmationVM.AppointmentHeader.TheName;
            appointmentFromDb.Number = ConfirmationVM.AppointmentHeader.Number;
            appointmentFromDb.City = ConfirmationVM.AppointmentHeader.City;
            appointmentFromDb.Region = ConfirmationVM.AppointmentHeader.Region;

            _unitOfWork.AppointmentHeader.Update(appointmentFromDb);

            TempData["Success"] = "Updated successfully";

            return RedirectToAction(nameof(Details),new { confirmId = appointmentFromDb });
        }

   


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<AppointmentHeader> objAppointmentHeader = 
                _unitOfWork.AppointmentHeader.GetAll(includeProperties: "ApplicationUser").ToList();

            switch (status)
            {
                case "pending":
                    objAppointmentHeader = objAppointmentHeader.Where(u => u.PaymentStatus == SD.PaymentStatusPending);
                    break;
                case "inprocess":
                    objAppointmentHeader = objAppointmentHeader.Where(u => u.PaymentStatus == SD.StatusPending);
                    break;
                case "completed":
                    objAppointmentHeader = objAppointmentHeader.Where(u => u.PaymentStatus == SD.StatusAproved);
                    break;
                case "approved":
                    objAppointmentHeader = objAppointmentHeader.Where(u => u.PaymentStatus == SD.StatusAproved);
                    break;
                default:
                    break;
         
            }
            return Json(new { data = objAppointmentHeader });
        }

        #endregion
    }
}
