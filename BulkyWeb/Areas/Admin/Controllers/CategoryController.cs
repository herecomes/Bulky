﻿using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
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
                ModelState.AddModelError("name", "Name should not be equal to Display order");
            }
            if (obj.Name != null && obj.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is invalid value");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created!";
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
            //Category? categoryFromDb = _unitOfWork.Category.Categories.Find(id);
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            //Category? categoryFromDb3 = _unitOfWork.Category.Categories.Where(u=>u.Id == id).FirstOrDefault();
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
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category Edited!";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category? categoryFromDb = _unitOfWork.Category.Categories.Find(id);
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            //Category? categoryFromDb3 = _unitOfWork.Category.Categories.Where(u=>u.Id == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted!";
            return RedirectToAction("Index");
        }
    }
}
