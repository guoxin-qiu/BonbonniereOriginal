using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bonbonniere.Core.Interfaces;

namespace Bonbonniere.Website.Features.MusicStore
{
    public class MusicStoreManagerController : Controller
    {
        private readonly IMusicStoreRepository _storeRepository;

        public MusicStoreManagerController(IMusicStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var albums = await _storeRepository.ListAsync();
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
