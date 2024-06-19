
using HandmadeShop.Data;
using HandmadeShop.Models;
using HandmadeShop.Models.ViewModels;
using HandmadeShop.Repository;
using Microsoft.AspNetCore.Mvc;


namespace HandmadeShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CategoryController : Controller
    {  
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult ProductsByCategory(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);
            var products = _context.Products.Where(p => p.CategoryId == categoryId).ToList();

            var model = new ProductsByCategoryVM
            {
                SelectedCategory = category,
                Products = products
            };

            return View(model);
        }
    }
}