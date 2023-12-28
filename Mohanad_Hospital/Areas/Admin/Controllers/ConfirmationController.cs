using Hospital.DataAccess.Repository.IRepository;
using Hospital.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Mohanad_Hospital.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ConfirmationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly IUnitOfWork _unitOfWork;
        
        public ConfirmationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<AppointmentHeader> objAppointmentHeader = _unitOfWork.AppointmentHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            return Json(new { data = objAppointmentHeader });
        }

        #endregion
    }
}
