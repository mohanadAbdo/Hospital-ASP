using Microsoft.AspNetCore.Mvc;
using Hospital.DataAccess.Data;

using Hospital.Models;
using Hospital.DataAccess.Repository.IRepository;

namespace Mohanad_Hospital.Controllers
{
    public class CategoryController : Controller
    {
        private readonly  ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository db)
        {
            _categoryRepo = db;
        
        }
        public IActionResult Index()
        {
            var objCategoryList = _categoryRepo.GetAll().ToList();
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
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
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

            Category? categoryFromDb = _categoryRepo.Get(u=>u.Id==id);

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
                _categoryRepo.Update(obj);
                _categoryRepo.Save();
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

            Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);

        }
        [HttpPost , ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _categoryRepo.Get(u => u.Id == id);
            _categoryRepo.Delete(obj);
            _categoryRepo.Save();
            if (obj == null)
            {
                return NotFound();
            }
           
                return RedirectToAction("Index", "Category");

        }
    }
}
