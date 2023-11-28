using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.DataAccess.Data;
using Book.DataAccess.Reponsitory.IReponsitory;
using Book.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryReponsitory _categoryRepo;

        public CategoryController(ICategoryReponsitory db)
        {
            _categoryRepo = db;
        }

        // GET: Category
        public IActionResult Index()
        {
            var objCategorylist = _categoryRepo.GetAll().ToList();
            return View(objCategorylist);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _categoryRepo.Add(obj);
                _categoryRepo.Save();
                TempData["success"] = "Category create successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u=>u.Id==id);
            // Category? categoryFromDb1 = _categoryRepo.Categories.FirstOrDefault(u=>u.Id == id);
            // Category? categoryFromDb2 = _categoryRepo.Categories.Where(u=>u.Id==id).FirstOrDefault();
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
                TempData["success"] = "Category update successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _categoryRepo.Get(u=>u.Id==id);
            // Category? categoryFromDb1 = _categoryRepo.Categories.FirstOrDefault(u=>u.Id == id);
            // Category? categoryFromDb2 = _categoryRepo.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        // không thể sử dụng chung tên và biến giống nhau cho hai thuộc tính GET và POST, vì vậy thay đổi tên của thuộc tính POST và chỉ định rằng Action name là tên của thuộc tính cần có như vậy sẽ không ảnh hưởng đến code
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _categoryRepo.Get(u=>u.Id==id);
            if (obj == null)
            {
                return NotFound();
            }
            _categoryRepo.Remove(obj);
            _categoryRepo.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
