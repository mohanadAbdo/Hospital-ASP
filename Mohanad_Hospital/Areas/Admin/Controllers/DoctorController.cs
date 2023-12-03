using Microsoft.AspNetCore.Mvc;
using Hospital.DataAccess.Data;

using Hospital.Models;
using Hospital.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.Models.ViewModels;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Mohanad_Hospital.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DoctorController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Doctor> objDoctorList = _unitofwork.Doctor.GetAll(includeProperties:"Category").ToList();

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
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file!=null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string doctorImagePath = Path.Combine(wwwRootPath, @"images\Doctors");

                    if (!string.IsNullOrEmpty(doctorVM.Doctor.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath,doctorVM.Doctor.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(doctorImagePath, fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream); 
                    }

                    doctorVM.Doctor.ImageUrl = @"\images\Doctors\" + fileName;
                }
                if (doctorVM.Doctor.Id == 0) 
                {
                    _unitofwork.Doctor.Add(doctorVM.Doctor);
                }
                else
                {
                    _unitofwork.Doctor.Update(doctorVM.Doctor);
                }
                _unitofwork.Doctor.Add(doctorVM.Doctor);
                _unitofwork.Save();
                return RedirectToAction("Index", "Doctor");
            }
            return View();
        }
       

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Doctor> objDoctorList = _unitofwork.Doctor.GetAll(includeProperties: "Category").ToList();
            return Json(new {data = objDoctorList });
        }
       
        public IActionResult Delete(int? id)
        {
            var doctorDelete = _unitofwork.Doctor.Get(u=>u.Id == id);
            if (doctorDelete == null)
            {
                return Json(new { success = false, message = "Error" });

            }
            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, doctorDelete.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitofwork.Doctor.Delete(doctorDelete);
            _unitofwork.Save();
            return Json(new { success = true, message ="Done" });
        }
        #endregion
    }
}
