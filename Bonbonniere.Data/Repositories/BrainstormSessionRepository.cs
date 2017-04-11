using Bonbonniere.Core.Interfaces;
using System;
using System.Collections.Generic;
using Bonbonniere.Core.Models;
using System.Threading.Tasks;
using Bonbonniere.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bonbonniere.Data.Repositories
{
    public class BrainstormSessionRepository : IBrainstormSessionRepository
    {
        private IDataProvider _dataProvider;
        private BonbonniereContext _dbContext => _dataProvider.DbContext;

        public BrainstormSessionRepository(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public Task AddAsync(BrainstormSession session)
        {
            _dbContext.BrainStormSessions.Add(session);
            return _dbContext.SaveChangesAsync();
        }

        public Task<BrainstormSession> GetByIdAsync(int id)
        {
            return _dbContext.BrainStormSessions
                .Include(s => s.Ideas)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<List<BrainstormSession>> ListAsync()
        {
            return _dbContext.BrainStormSessions
                .Include(s => s.Ideas)
                .OrderByDescending(s => s.DateCreated)
                .ToListAsync();
        }

        public Task UpdateAsync(BrainstormSession session)
        {
            _dbContext.Entry(session).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }
    }
}
