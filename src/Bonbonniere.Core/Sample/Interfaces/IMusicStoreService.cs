using Bonbonniere.Core.Paging;
using Bonbonniere.Core.Sample.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bonbonniere.Core.Sample.Interfaces
{
    public interface IMusicStoreService
    {
        Task<List<Album>> GetListAsync();

        Task<List<Genre>> GetTopGenresAsync(int top);

        PaginatedList<Album> GetPagedList(string title, string sortOrder, int? page, int pageSize);
    }
}
