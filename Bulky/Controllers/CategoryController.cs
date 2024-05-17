using Bulky.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var objCategoryList=_db.Categories.ToList();
            return View(objCategoryList);
        }
		public IActionResult Create()
		{
			return View();
		}

  //      [HttpPost]
		//public IActionResult Create()
		//{
  //          if (category == null)
  //          {

  //          }

		//}
	}
}
