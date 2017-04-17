using Microsoft.AspNetCore.Mvc;

namespace Bonbonniere.Website.Areas.WordTutor.Controllers
{
    [Area("WordTutor")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}