using Bonbonniere.Core.Paging;
using Bonbonniere.Core.Sample.Interfaces;
using Bonbonniere.Core.Sample.Model;
using Bonbonniere.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bonbonniere.Services.Implementations
{
    public class MusicStoreService : IMusicStoreService
    {
        private readonly SampleContext _context;

        public MusicStoreService(SampleContext context)
        {
            _context = context;
        }

        public Task<List<Album>> GetListAsync()
        {
            return _context.Albums.ToListAsync();
        }

        public PaginatedList<Album> GetPagedList(string title, string sortOrder, int? page, int pageSize)
        {
            var query = _context.Albums.AsQueryable();
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
            return _context.Genres.OrderBy(t => t.Name).Take(top).ToListAsync();
        }
    }
}
