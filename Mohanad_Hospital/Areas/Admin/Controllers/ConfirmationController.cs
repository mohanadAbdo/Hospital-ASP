using Hospital.DataAccess.Repository.IRepository;
using Hospital.Models;
using Hospital.Models.ViewModels;
using Hospital.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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

        [HttpPost]
        [Authorize(Roles = SD.Admin_Role)]
        public IActionResult Aprove()
        {
            _unitOfWork.AppointmentHeader.UpdateStatus(ConfirmationVM.AppointmentHeader.Id,SD.StatusAproved);
            _unitOfWork.Save();
            TempData["Success"] = "Updated successfully";

            return RedirectToAction(nameof(Details), new { confirmationId = ConfirmationVM.AppointmentHeader.Id });
        }

        [HttpPost]
        [Authorize(Roles = SD.Admin_Role)]
        public IActionResult Cancel()
        {
            var appointmentHeader =  _unitOfWork.AppointmentHeader.
                Get(u=>u.Id ==ConfirmationVM.AppointmentHeader.Id);

            _unitOfWork.AppointmentHeader.UpdateStatus(appointmentHeader.Id,SD.StatusCancelled,SD.StatusCancelled);
            _unitOfWork.Save();
            TempData["Success"] = "Updated successfully";

            return RedirectToAction(nameof(Details), new { confirmationId = ConfirmationVM.AppointmentHeader.Id });

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int confirmationId)
        {
            ConfirmationVM = new ()
            {
                AppointmentHeader = _unitOfWork.AppointmentHeader.Get(u => u.Id == confirmationId, includeProperties: "ApplicationUser"),
                AppointmentDetail = _unitOfWork.AppointmentDetail.GetAll(u => u.AppointmentHeaderId == confirmationId, includeProperties: "Doctor")
            };

            return View(ConfirmationVM);
        }

        [HttpPost]
        [Authorize(Roles =SD.Admin_Role)]
        public IActionResult UpdateConfirmationDetail(int confirmationId)
        {
            var appointmentFromDb = _unitOfWork.AppointmentHeader.Get(u => u.Id == ConfirmationVM.AppointmentHeader.Id);
            appointmentFromDb.TheName = ConfirmationVM.AppointmentHeader.TheName;
            appointmentFromDb.Number = ConfirmationVM.AppointmentHeader.Number;
            appointmentFromDb.City = ConfirmationVM.AppointmentHeader.City;
            appointmentFromDb.Region = ConfirmationVM.AppointmentHeader.Region;

            _unitOfWork.AppointmentHeader.Update(appointmentFromDb);
            _unitOfWork.Save();
            TempData["Success"] = "Updated successfully";

            return RedirectToAction(nameof(Details),new { confirmationId = appointmentFromDb });
        }

        


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<AppointmentHeader> objAppointmentHeader = 
                _unitOfWork.AppointmentHeader.GetAll(includeProperties: "ApplicationUser").ToList();

            if(User.IsInRole(SD.Admin_Role))
            {
                objAppointmentHeader = _unitOfWork.AppointmentHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
                objAppointmentHeader= _unitOfWork.AppointmentHeader.
                    GetAll(u=>u.ApplicationUserId== userId,includeProperties: "ApplicationUser");
            }
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
                case "aproved":
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
