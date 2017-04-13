using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bonbonniere.Core.Interfaces;
using Bonbonniere.Website.Areas.MusicStore.ViewModels;

namespace Bonbonniere.Website.Areas.MusicStore.Controllers
{
    [Area("MusicStore")]
    public class StoreManagerController : Controller
    {
        private readonly IMusicStoreRepository _storeRepository;

        public StoreManagerController(IMusicStoreRepository storeRepository)
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