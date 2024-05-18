using Datas;
using Models;
using Microsoft.AspNetCore.Mvc;
using Datas.Repository.IRepository;

namespace Bulky.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductController(IUnitOfWork db)
		{
			_unitOfWork = db;
		}
		public IActionResult Index()
		{
			var objProductList = _unitOfWork.Product.GetAll();
			return View(objProductList);
		}
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Product obj)
		{

			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Add(obj);
				_unitOfWork.Save();
				TempData["success"] = "Product Created Successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}
		[HttpPost]
		public IActionResult Edit(Product obj)
		{
			if (obj != null && ModelState.IsValid)
			{
				_unitOfWork.Product.Update(obj);
				_unitOfWork.Save();
				TempData["success"] = "Product Updated Successfully";
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
			Product ProductFromDb = _unitOfWork.Product.Get(x => x.Id == id);
			if (ProductFromDb == null) return NotFound();
			return View(ProductFromDb);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(Product ProductSelected)
		{
			if (ProductSelected == null) return NotFound();
			_unitOfWork.Product.Remove(ProductSelected);
			_unitOfWork.Save();
			TempData["success"] = "Product Deleted Successfully";
			return RedirectToAction("Index");
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Product ProductFromDb = _unitOfWork.Product.Get(x => x.Id == id);
			if (ProductFromDb == null) return NotFound();
			return View(ProductFromDb);
		}
	}
}
