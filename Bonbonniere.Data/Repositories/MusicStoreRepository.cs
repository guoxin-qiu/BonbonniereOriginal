using Bonbonniere.Core.Interfaces;
using System.Collections.Generic;
using Bonbonniere.Core.Models.MusicStore;
using System.Threading.Tasks;
using Bonbonniere.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Data.Repositories
{
    public class MusicStoreRepository : IMusicStoreRepository
    {
        private IDataProvider _dataProvider;
        private BonbonniereContext _dbContext => _dataProvider.DbContext;

        public MusicStoreRepository(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public Task<List<Album>> ListAsync()
        {
            return _dbContext.Set<Album>().ToListAsync();
        }
    }
}
