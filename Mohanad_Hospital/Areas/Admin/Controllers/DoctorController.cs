using Microsoft.AspNetCore.Mvc;
using Hospital.DataAccess.Data;

using Hospital.Models;
using Hospital.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.Models.ViewModels;

namespace Mohanad_Hospital.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public DoctorController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;

        }
        public IActionResult Index()
        {
            List<Doctor> objDoctorList = _unitofwork.Doctor.GetAll().ToList();

            return View(objDoctorList);
        }
        public IActionResult Upsert(int? id)
        {
            DoctorViewModel doctorVM = new()
            {
                CategoryList = _unitofwork.Category.GetAll().Select(u => new SelectListItem
             {
                 Text = u.Name,
                 Value = u.Id.ToString()
             }
                ),
                Doctor = new Doctor()
            };
            if(id==null || id == 0)
            {
                //Create
                return View(doctorVM);

            }
            else
            {
                //Update
                doctorVM.Doctor = _unitofwork.Doctor.Get(u => u.Id == id);
                return View(doctorVM); 
            }
        }
        [HttpPost]
        public IActionResult Upsert(DoctorViewModel doctorVM , IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.Doctor.Add(doctorVM.Doctor);
                _unitofwork.Save();
                return RedirectToAction("Index", "Doctor");
            }
            return View();
        }
       
        public IActionResult Delete(int? id)
        {
            // Assuming 'id' is a parameter passed into this code block
            if (id == null)
            {
                return NotFound();
            }

            Doctor? DoctorFromDb = _unitofwork.Doctor.Get(u => u.Id == id);

            if (DoctorFromDb == null)
            {
                return NotFound();
            }

            return View(DoctorFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Doctor? obj = _unitofwork.Doctor.Get(u => u.Id == id);
            _unitofwork.Doctor.Delete(obj);
            _unitofwork.Save();
            if (obj == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Doctor");

        }
    }
}
