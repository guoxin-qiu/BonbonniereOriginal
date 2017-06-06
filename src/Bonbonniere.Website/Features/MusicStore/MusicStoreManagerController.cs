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

        public async Task<IActionResult> Index(string titleSearch, string sortOrder)
        {
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["CurrentFilter"] = titleSearch ?? "";

            var albums = await _musicStoreService.GetListAsync(titleSearch, sortOrder);
            var model = albums.Select(a => new AlbumsViewModel
            {
                AlbumArtUrl = a.ArtUrl,
                AlbumId = a.Id,
                AlbumPrice = a.Price,
                AlbumReleaseDate = a.ReleaseDate,
                AlbumTitle = a.Title
            }).ToList();

            return View(model);
        }
    }
}
