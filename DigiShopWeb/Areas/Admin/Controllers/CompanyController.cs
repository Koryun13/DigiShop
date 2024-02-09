using Microsoft.AspNetCore.Mvc;
using DigiShop.DataAccess.Data;
using DigiShop.Models;
using DigiShop.DataAccess.Repository;
using DigiShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using DigiShop.Models.ViewModels;
using Microsoft.VisualStudio.Web.CodeGeneration;
using DigiShop.Utility;
using Microsoft.AspNetCore.Authorization;

namespace DigiShopWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CompanyController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        List<Company> objProductList = _unitOfWork.Company.GetAll(includeProperties: "Category").ToList();

        return View(objProductList);
    }


    public IActionResult Upsert(int? id)
    {

        if (id == null || id == 0)
        {
            return View(new Company());
        }
        else
        {
            Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
            return View(companyObj);
        }
    }

    [HttpPost]
    public IActionResult Upsert(Company companyObj)
    {

        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

           
            if (companyObj.Id == 0)
            {
                _unitOfWork.Company.Add(companyObj);
            }
            else
            {
                _unitOfWork.Company.Update(companyObj);
            }
            _unitOfWork.Save();
            TempData["success"] = "Company created successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    #region API CALLS 

    public IActionResult GetAll()
    {
        List<Company> objProductList = _unitOfWork.Company.GetAll().ToList();
        return Json(new { data = objProductList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var productToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);

        if (productToBeDeleted == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        _unitOfWork.Company.Remove(productToBeDeleted);
        _unitOfWork.Save();

        return Json(new { success = true, message = "Delete Successful" });
    }
    #endregion
}
