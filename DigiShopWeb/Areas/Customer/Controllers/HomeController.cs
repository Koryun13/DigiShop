using DigiShop.DataAccess.Repository.IRepository;
using DigiShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace DigiShopWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(productList);

        }
        public IActionResult Details(int id)
        {
            ShoppingCard cart = new()
            {
                Product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "Category"),
                Count = 1,
                ProductId = id
            };

            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCard shoppingCard)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCard.ApplicationUserId = userId;

            ShoppingCard cardFromDb = _unitOfWork.ShoppingCard.Get(u => u.ApplicationUserId == userId && 
            u.ProductId == shoppingCard.ProductId);

            if(cardFromDb != null) 
            {
                cardFromDb.Count += shoppingCard.Count;
                _unitOfWork.ShoppingCard.Update(cardFromDb);
            }
            else
            {
                _unitOfWork.ShoppingCard.Add(shoppingCard);
            }

            _unitOfWork.Save();

            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}