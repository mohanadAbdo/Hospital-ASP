using Microsoft.AspNetCore.Mvc;
using Hospital.DataAccess.Data;

using Hospital.Models;
using Hospital.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitofwork.Category.
             GetAll().Select(u => new SelectListItem
                {
             Text = u.Name,
             Value = u.Id.ToString()
             }
                );
            // ViewBag.CategoryList = CategoryList;
            ViewData["CategoryList"] = CategoryList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.Doctor.Add(obj);
                _unitofwork.Save();
                return RedirectToAction("Index", "Doctor");
            }
            return View();
        }
        public IActionResult Edit(int? id)
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
        [HttpPost]
        public IActionResult Edit(Doctor obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.Doctor.Update(obj);
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
