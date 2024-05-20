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
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ProductController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = db;
			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			var objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category");
			return View(objProductList);
		}
		public IActionResult Upsert(int? id)
		{
			ProductVM productVM = new ProductVM();
			productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString()
			});
			if (id == null || id == 0)
			{
				productVM.Product = new Product();
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
				if (file is not null)
				{

					string wwwRootPath = _webHostEnvironment.WebRootPath;
					string dummyFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string dummyFilePath = Path.Combine(wwwRootPath, @"Images\Product");
					if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
					{
						var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}
					using (var fileStream = new FileStream(Path.Combine(dummyFilePath, dummyFileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					productVM.Product.ImageUrl = @"\Images\Product\" + dummyFileName;
				}
				if (productVM.Product.Id != null)
				{
					_unitOfWork.Product.Update(productVM.Product);
					TempData["success"] = "Product Updated Successfully";
				}
				else
				{
					_unitOfWork.Product.Add(productVM.Product);
					TempData["success"] = "Product Created Successfully";
				}
				_unitOfWork.Save();
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

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(Product ProductSelected)
		{
			if (ProductSelected == null) return NotFound();
			_unitOfWork.Product.Remove(ProductSelected);
			if (!string.IsNullOrEmpty(ProductSelected.ImageUrl))
			{
				var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, ProductSelected.ImageUrl.TrimStart('\\'));
				if (System.IO.File.Exists(oldImagePath))
				{
					System.IO.File.Delete(oldImagePath);
				}
			}
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
		#region APICALLS
		[HttpGet]
		public IActionResult GetAll(int id)
		{
			var objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category");
			return Json(new { data = objProductList });
		}
		#endregion
	}
}
