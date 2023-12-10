using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.DataAccess.Data;
using Book.DataAccess.Reponsitory.IReponsitory;
using Book.Models;
using Book.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Product
        public IActionResult Index()
        {
           
            var objProductlist = _unitOfWork.Product.GetAll().ToList();
            return View(objProductlist);
        }

        public IActionResult Create()
        {
            // ViewBag.CategoryList = CategoryList;
            ProductVM productVM = new() {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            }),
                Product = new Product()
            };
            return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product create successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            // Product? ProductFromDb1 = _unitOfWork.Categories.FirstOrDefault(u=>u.Id == id);
            // Product? ProductFromDb2 = _unitOfWork.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product update successfully";
                return RedirectToAction("Index", "Product");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            // Product? ProductFromDb1 = _unitOfWork.Categories.FirstOrDefault(u=>u.Id == id);
            // Product? ProductFromDb2 = _unitOfWork.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        // không thể sử dụng chung tên và biến giống nhau cho hai thuộc tính GET và POST, vì vậy thay đổi tên của thuộc tính POST và chỉ định rằng Action name là tên của thuộc tính cần có như vậy sẽ không ảnh hưởng đến code
        public IActionResult DeletePOST(int? id)
        {
            Product obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
