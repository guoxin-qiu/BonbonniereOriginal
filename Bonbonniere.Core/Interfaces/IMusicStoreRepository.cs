using Bonbonniere.Core.Models.MusicStore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bonbonniere.Core.Interfaces
{
    public interface IMusicStoreRepository
    {
        Task<List<Album>> ListAsync();
    }
}
