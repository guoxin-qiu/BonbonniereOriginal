using Bonbonniere.Infrastructure.FileSystem;
using Bonbonniere.Infrastructure.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Bonbonniere.Website.Features.Home
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly IImageService _imageService;
        private readonly IAppLogger<HomeController> _logger;

        public HomeController(IHostingEnvironment env, 
            IImageService imageService,
            IAppLogger<HomeController> logger)
        {
            _env = env;
            _imageService = imageService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpGet("[controller]/images/{id}")]
        public IActionResult GetImage(string id)
        {
            byte[] imageBytes;
            try
            {
                imageBytes = _imageService.GetImageBytesById(id);
            }
            catch (CatalogImageMissingException)
            {
                _logger.LogWarning($"No image found for id: {id}");
                return NotFound();
            }
            return File(imageBytes, "image/png");
        }
    }
}
