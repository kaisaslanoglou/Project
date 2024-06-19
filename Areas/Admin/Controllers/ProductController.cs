using HandmadeShop.Data;
using HandmadeShop.Models;
using HandmadeShop.Models.ViewModels;
using HandmadeShop.Repository;
using HandmadeShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System.Drawing;

namespace HandmadeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> objProductList = (await _unitOfWork.ProductRepository.GetAllAsync(includeProperties:"Category")).ToList();
           
            return View(objProductList);
        }


        public async Task<IActionResult> CreateUpdate(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = (await _unitOfWork.CategoryRepository.GetAllAsync()).Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = await _unitOfWork.ProductRepository.GetAsync(u => u.Id == id);
                return View(productVM);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateUpdate(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    }
                    // upload new image
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // update the imageUrl
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }
                else
                {
                    if(string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {
                        // set default empty image
                        productVM.Product.ImageUrl = @"\images\product\empty.png"; 
                    }                  
                }
               

                if (productVM.Product.Id == 0)
                {
                    // create
                    await _unitOfWork.ProductRepository.AddAsync(productVM.Product);
                }

                else
                {
                    //update
                    await _unitOfWork.ProductRepository.UpdateAsync(productVM.Product);
                }

                await _unitOfWork.SaveAsync();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            return View(productVM);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Product> objProductList = (await _unitOfWork.ProductRepository.GetAllAsync(includeProperties: "Category")).ToList();
            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            var productToDelete = await _unitOfWork.ProductRepository.GetAsync(u => u.Id == id);
            if (productToDelete == null)
            {
                return Json(new { success = false, message = "Failed to delete" });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,
                           productToDelete.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            await _unitOfWork.ProductRepository.DeleteAsync(productToDelete);
            await _unitOfWork.SaveAsync();

            return Json(new { success = true, message = "Product deleted!" });
        }
    }
}

