using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bonbonniere.Services.Interfaces;
using Bonbonniere.Infrastructure.Paging;
using System.Collections.Generic;

namespace Bonbonniere.Website.Features.MusicStore
{
    public class MusicStoreManagerController : Controller
    {
        private readonly IMusicStoreService _musicStoreService;

        public MusicStoreManagerController(IMusicStoreService musicStoreService)
        {
            _musicStoreService = musicStoreService;
        }

        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "price" ? "price_desc" : "price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString ?? "";

            var albums = _musicStoreService.GetPagedList(searchString, sortOrder, page, 3);
            var model = albums.Select(a => new AlbumsViewModel
            {
                AlbumArtUrl = a.ArtUrl,
                AlbumId = a.Id,
                AlbumPrice = a.Price,
                AlbumReleaseDate = a.ReleaseDate,
                AlbumTitle = a.Title
            }).ToList();

            return View(new PaginatedList<AlbumsViewModel>(model, albums.TotalItems, albums.PageIndex, albums.PageSize));
        }
    }
}
