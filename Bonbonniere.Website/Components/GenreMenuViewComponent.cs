using Bonbonniere.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace Bonbonniere.Website.Components
{
    [ViewComponent(Name = "GenreMenu")]
    public class GenreMenuViewComponent : ViewComponent
    {
        private IMusicStoreRepository _musicStoreRepository;
        public GenreMenuViewComponent(IMusicStoreRepository musicStoreRepository)
        {
            _musicStoreRepository = musicStoreRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = await _musicStoreRepository.GetTopGenresAsync(8);

            return View(genres.Select(t => t.Name));
        }
    }
}
