using Bonbonniere.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bonbonniere.Services.Interfaces;
using Bonbonniere.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Services.Implementations
{
    public class BrainstormService : ServiceBase, IBrainstormService
    {
        public BrainstormService(IDataProvider dataProvider) : base(dataProvider)
        {

        }

        public Task AddSessionAsync(BrainstormSession session)
        {
            _context.BrainstormSessions.Add(session);
            return _context.SaveChangesAsync();
        }

        public Task<BrainstormSession> GetByIdAsync(int id)
        {
            return _context.BrainstormSessions.Include(t => t.Ideas).FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<List<BrainstormSession>> GetListAsync()
        {
            return _context.BrainstormSessions.Include(t => t.Ideas).ToListAsync();
        }

        public Task UpdateAsync(BrainstormSession session)
        {
            _context.BrainstormSessions.Update(session);
            return _context.SaveChangesAsync();
        }
    }
}
