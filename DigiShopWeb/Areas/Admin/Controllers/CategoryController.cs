using Microsoft.AspNetCore.Mvc;
using DigiShop.DataAccess.Data;
using DigiShop.Models;
using DigiShop.DataAccess.Repository;
using DigiShop.DataAccess.Repository.IRepository;

namespace DigiShopWeb.Areas.Admin.Controllers;

[Area("Admin")]
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
    public IActionResult Create(Category cateoryOrder)
    {
        //_db.Categories.Add(cateoryOrder);
        //_db.SaveChanges();
        //return RedirectToAction("Index","Category");
        if (cateoryOrder.Name == cateoryOrder.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(cateoryOrder);
            _unitOfWork.Save();


            TempData["success"] = "Category created successfully";
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
        Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
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
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category edited successfully";
            return RedirectToAction("Index");
        }
#if DEBUG
        else
        {
            foreach (var modelStateKey in ModelState.Keys)
            {
                var modelStateVal = ModelState[modelStateKey];
                foreach (var error in modelStateVal.Errors)
                {
                    Console.WriteLine($"Validation error for {modelStateKey}: {error.ErrorMessage}");
                }
            }
        }
#endif
        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }

    [HttpPost]
    public IActionResult Delete(Category obj)
    {
        _unitOfWork.Category.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}
