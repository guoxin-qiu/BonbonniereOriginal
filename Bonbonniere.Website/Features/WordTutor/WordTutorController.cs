using Microsoft.AspNetCore.Mvc;

namespace Bonbonniere.Website.Features.WordTutor
{
    public class WordTutorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
