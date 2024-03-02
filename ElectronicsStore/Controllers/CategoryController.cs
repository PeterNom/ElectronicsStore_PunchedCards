using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
