using Microsoft.AspNetCore.Mvc;

namespace DigiShop.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
