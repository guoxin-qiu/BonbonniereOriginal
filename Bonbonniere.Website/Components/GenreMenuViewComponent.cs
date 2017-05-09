using Bonbonniere.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Bonbonniere.Website.Components
{
    [ViewComponent(Name = "GenreMenu")]
    public class GenreMenuViewComponent : ViewComponent
    {
        private readonly IMusicStoreRepository _musicStoreRepository;
        private readonly ILogger<GenreMenuViewComponent> _logger;
        public GenreMenuViewComponent(IMusicStoreRepository musicStoreRepository, ILogger<GenreMenuViewComponent> logger)
        {
            _musicStoreRepository = musicStoreRepository;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            _logger.LogInformation("----- Invoke component genre-menu.");
            var genres = await _musicStoreRepository.GetTopGenresAsync(8);

            return View(genres.Select(t => t.Name).ToList());
        }
    }
}
