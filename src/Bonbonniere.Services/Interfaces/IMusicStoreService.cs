using Bonbonniere.Core.Models.MusicStore;
using Bonbonniere.Infrastructure.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bonbonniere.Services.Interfaces
{
    public interface IMusicStoreService
    {
        Task<List<Album>> GetListAsync();

        Task<List<Genre>> GetTopGenresAsync(int top);

        PaginatedList<Album> GetPagedList(string title, string sortOrder, int? page, int pageSize);
    }
}
