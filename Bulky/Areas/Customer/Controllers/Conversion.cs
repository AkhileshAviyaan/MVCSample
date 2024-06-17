using Microsoft.AspNetCore.Mvc;

namespace Bulky.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class Conversion : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
