using DigiShop.Data;
using DigiShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigiShop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();

            return View(objCategoryList);
        }
     

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category cateoryOrder)
        {
            _db.Categories.Add(cateoryOrder);
            _db.SaveChanges();
            return RedirectToAction("Index","Category");
        }

    }
}
