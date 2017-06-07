using Bonbonniere.Core.Models.MusicStore;
using Bonbonniere.Infrastructure.Domain;
using Bonbonniere.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bonbonniere.Infrastructure.Paging;
using System.Linq;

namespace Bonbonniere.Services.Implementations
{
    public class MusicStoreService : IMusicStoreService
    {
        private readonly IRepository<Album> _albumRepository;
        private readonly IRepository<Genre> _genreRepository;
        private readonly IUnitOfWork _uow;

        public MusicStoreService(
            IRepository<Album> albumRepository,
            IRepository<Genre> genreRepository,
            IUnitOfWork uow)
        {
            _albumRepository = albumRepository;
            _genreRepository = genreRepository;
            _uow = uow;
        }

        public Task<List<Album>> GetListAsync()
        {
            return _albumRepository.FetchAllAsync();
        }

        public Task<List<Album>> GetListAsync(string title, string sortOrder)
        {
            Action<Orderable<Album>> order = o => o.Asc(r => r.Title);
            switch (sortOrder)
            {
                case "title_desc":
                    order = o => o.Desc(r => r.Title);
                    break;
                case "price":
                    order = o => o.Asc(r => r.Price);
                    break;
                case "price_desc":
                    order = o => o.Desc(r => r.Price);
                    break;
                default:
                    break;
            }

            return _albumRepository.FetchOrderedAsync(t => string.IsNullOrWhiteSpace(title) || t.Title.Contains(title), order);
        }

        public PaginatedList<Album> GetPagedList(string title, string sortOrder, int? page, int pageSize)
        {
            var query = _albumRepository.FetchAllQueryable();
            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(t => t.Title.Contains(title));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    query = query.OrderByDescending(t => t.Title);
                    break;
                case "price":
                    query = query.OrderBy(t => t.Price);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(t => t.Price);
                    break;
                default:
                    query = query.OrderBy(t => t.Title);
                    break;
            }

            return PaginatedList<Album>.Create(query, page ?? 1, pageSize);
        }

        public Task<List<Genre>> GetTopGenresAsync(int top)
        {
            return _genreRepository.FetchAllOrderedAsync(o => o.Asc(r => r.Name), 0, top);
        }
    }
}
