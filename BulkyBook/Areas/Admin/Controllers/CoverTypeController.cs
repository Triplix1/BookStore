using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork context)
        {
            _unitOfWork = context;
        }

        public IActionResult Index()
        {
            var list = _unitOfWork.CoverType.GetAll();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(coverType);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type created successfully";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var coverType = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (coverType == null)
                return NotFound();

            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(coverType);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type Edited successfully";
                return RedirectToAction(nameof(Index));
            }

            return View(coverType);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var coverType = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);

            if (coverType == null)
                return NotFound();

            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CoverType coverType)
        {
            if (coverType == null)
                return NotFound();

            _unitOfWork.CoverType.Remove(coverType);
            _unitOfWork.Save();

            TempData["success"] = "Cover Type Deleted successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
