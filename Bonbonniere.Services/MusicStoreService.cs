using Bonbonniere.Core.Models.MusicStore;
using Bonbonniere.Infrastructure.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bonbonniere.Services
{
    public interface IMusicStoreService
    {
        Task<List<Album>> GetListAsync();

        Task<List<Genre>> GetTopGenresAsync(int top);
    }

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

        public Task<List<Genre>> GetTopGenresAsync(int top)
        {
            return _genreRepository.FetchAllOrderedAsync(o => o.Asc(r => r.Name), 0, top);
        }
    }
}
