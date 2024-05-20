using Datas;
using Models;
using Microsoft.AspNetCore.Mvc;
using Datas.Repository.IRepository;
using Npgsql;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.ViewModels;
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
		public IActionResult Upsert(int? id)
		{
			ProductVM productVM = new ProductVM();
			if (id == null || id == 0)
			{
				productVM.Product = new Product();
				productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});
			}
			else
			{
				productVM.Product = _unitOfWork.Product.Get(x => x.Id == id);
			}
				return View(productVM);
		}

		[HttpPost]
		public IActionResult Upsert(ProductVM productVM, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Add(productVM.Product);
				_unitOfWork.Save();
				TempData["success"] = "Product Created Successfully";
				return RedirectToAction("Index");
			}
			else
			{
				productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				});
				return View(productVM);
			}
		}
		[HttpPost]
		public IActionResult Edit(Product obj)
		{
			if (obj != null & ModelState.IsValid)
			{
				_unitOfWork.Product.Update(obj);
				try
				{
					_unitOfWork.Save();
					TempData["success"] = "Product Updated Successfully";
				}
				catch (PostgresException ex) { }
				return RedirectToAction("Index");
			}
			return View(obj);
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
