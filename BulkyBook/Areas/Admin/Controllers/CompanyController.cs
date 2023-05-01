using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CompanyController(IUnitOfWork context, IWebHostEnvironment env)
        {
            _unitOfWork = context;
            _hostEnvironment = env;
        }

        public IActionResult Index()
        {
            var list = _unitOfWork.Company.GetAll();
            return View(list);
        }

        public IActionResult Upsert(int? id)
        {
            Company company = new();           
           
            if (id == null || id == 0)
            {
                //create model
                return View(company);
            }
            else
            {
                company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                return View(company);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {                

                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company);                    
                    TempData["success"] = "Company cerated successfully";
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                    TempData["success"] = "Company Edited successfully";
                }

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(company);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Company.GetAll();
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
                        
            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Deleted successful" });
        }
        #endregion
    }
}
