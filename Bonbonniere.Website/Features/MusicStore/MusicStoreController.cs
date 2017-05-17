using Microsoft.AspNetCore.Mvc;

namespace Bonbonniere.Website.Features.MusicStore
{
    public class MusicStoreController : Controller
    {
        public IActionResult Index()
        {
            return Content("Welcome to Music Store.");
        }
    }
}
