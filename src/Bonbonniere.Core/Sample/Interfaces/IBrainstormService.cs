using Bonbonniere.Core.Sample.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bonbonniere.Core.Sample.Interfaces
{
    public interface IBrainstormService
    {
        Task<List<BrainstormSession>> GetListAsync();
        Task AddSessionAsync(BrainstormSession session);
        Task<BrainstormSession> GetByIdAsync(int id);
        Task UpdateAsync(BrainstormSession session);
    }
}
