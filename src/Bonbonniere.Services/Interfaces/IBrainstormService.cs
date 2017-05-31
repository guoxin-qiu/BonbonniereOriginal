using Bonbonniere.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bonbonniere.Services.Interfaces
{
    public interface IBrainstormService
    {
        Task<List<BrainstormSession>> GetListAsync();
        Task AddSessionAsync(BrainstormSession session);
        Task<BrainstormSession> GetByIdAsync(int id);
        Task UpdateAsync(BrainstormSession session);
    }
}
