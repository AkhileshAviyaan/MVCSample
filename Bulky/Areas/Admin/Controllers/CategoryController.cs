using Datas;
using Models;
using Microsoft.AspNetCore.Mvc;
using Datas.Repository.IRepository;

namespace Bulky.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            var objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category catobj)
        {
            if (catobj.Name == catobj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "DisplayOrder must not be equal to Name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(catobj);
                _unitOfWork.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(catobj);

        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
				TempData["success"] = "Category Updated Successfully";
				return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _unitOfWork.Category.Get(x => x.Id == id);
            if (categoryFromDb == null) return NotFound();
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Category categorySelected)
        {
            if (categorySelected == null) return NotFound();
            _unitOfWork.Category.Remove(categorySelected);
            _unitOfWork.Save();
			TempData["success"] = "Category Deleted Successfully";
			return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _unitOfWork.Category.Get(x => x.Id == id);
            if (categoryFromDb == null) return NotFound();
            return View(categoryFromDb);
        }
    }
}
