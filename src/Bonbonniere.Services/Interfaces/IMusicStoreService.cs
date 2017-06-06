using Bonbonniere.Core.Models.MusicStore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bonbonniere.Services.Interfaces
{
    public interface IMusicStoreService
    {
        Task<List<Album>> GetListAsync();

        Task<List<Album>> GetListAsync(string title, string sortOrder);

        Task<List<Genre>> GetTopGenresAsync(int top);
    }
}
