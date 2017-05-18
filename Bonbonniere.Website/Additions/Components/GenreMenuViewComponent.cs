using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using Bonbonniere.Services;

namespace Bonbonniere.Website.Additions.Components
{
    [ViewComponent(Name = "GenreMenu")]
    public class GenreMenuViewComponent : ViewComponent
    {
        private readonly IMusicStoreService _musicStoreService;
        private readonly ILogger<GenreMenuViewComponent> _logger;

        public GenreMenuViewComponent(
            IMusicStoreService musicStoreService, 
            ILogger<GenreMenuViewComponent> logger)
        {
            _musicStoreService = musicStoreService;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            _logger.LogInformation("----- Invoke component genre-menu.");
            var genres = await _musicStoreService.GetTopGenresAsync(8);
            return View(genres.Select(t => t.Name).ToList());
        }
    }
}
