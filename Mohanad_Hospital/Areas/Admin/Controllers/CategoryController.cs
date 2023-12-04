using Microsoft.AspNetCore.Mvc;
using Hospital.DataAccess.Data;

using Hospital.Models;
using Hospital.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Hospital.Utility;

namespace Mohanad_Hospital.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Admin_Role)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;

        }
        public IActionResult Index()
        {
            var objCategoryList = _unitofwork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.Category.Add(obj);
                _unitofwork.Save();
                return RedirectToAction("Index", "Category");
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

            Category? categoryFromDb = _unitofwork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);

        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.Category.Update(obj);
                _unitofwork.Save();
                return RedirectToAction("Index", "Category");
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

            Category? categoryFromDb = _unitofwork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _unitofwork.Category.Get(u => u.Id == id);
            _unitofwork.Category.Delete(obj);
            _unitofwork.Save();
            if (obj == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Category");

        }
    }
}
