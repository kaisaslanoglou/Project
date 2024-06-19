using HandmadeShop.Data;
using HandmadeShop.Models;
using HandmadeShop.Repository;
using HandmadeShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace HandmadeShop.Areas.Admin.Controllers
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

        

        public async Task<IActionResult> Index()
        {
            List<Category> objCategoryList = (await _unitOfWork.CategoryRepository.GetAllAsync()).ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order can not match the Name");
            }
            if (ModelState.IsValid)
            {
                await _unitOfWork.CategoryRepository.AddAsync(category);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryToEdit = await _unitOfWork.CategoryRepository.GetAsync(u => u.Id == id);

            if (categoryToEdit == null)
            {
                return NotFound();
            }

            return View(categoryToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.CategoryRepository.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryToDelete = await _unitOfWork.CategoryRepository.GetAsync(u => u.Id == id);

            if (categoryToDelete == null)
            {
                return NotFound();
            }

            return View(categoryToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            Category? category = await _unitOfWork.CategoryRepository.GetAsync(u => u.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            await _unitOfWork.CategoryRepository.DeleteAsync(category);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }


    }
}
