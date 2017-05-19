using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bonbonniere.Services.Interfaces;

namespace Bonbonniere.Website.Features.MusicStore
{
    public class MusicStoreManagerController : Controller
    {
        private readonly IMusicStoreService _musicStoreService;

        public MusicStoreManagerController(IMusicStoreService musicStoreService)
        {
            _musicStoreService = musicStoreService;
        }

        public async Task<IActionResult> Index()
        {
            var albums = await _musicStoreService.GetListAsync();
            var model = albums.Select(a => new AlbumsViewModel
            {
                AlbumArtUrl = a.ArtUrl,
                AlbumId = a.Id,
                AlbumPrice = a.Price,
                AlbumTitle = a.Title
            }).ToList();

            return View(model);
        }
    }
}
