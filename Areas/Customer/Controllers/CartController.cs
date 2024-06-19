using Microsoft.AspNetCore.Mvc;

namespace HandmadeShop.Areas.Customer.Controllers
{
    public class CartController : Controller
    { 
        [HttpGet]
        public IActionResult GetCartCount()
        {
            int currentCartCount = HttpContext.Session.GetInt32("CartItemCount") ?? 0;
            return Json(currentCartCount);
        }

        [HttpPost]
        public IActionResult UpdateCartCount(int productId, int quantity)
        {
            int currentCount = HttpContext.Session.GetInt32("CartCount") ?? 0;
            currentCount += quantity;
            HttpContext.Session.SetInt32("CartCount", currentCount);

            return Json(new { success = true });
        }
    }
}

