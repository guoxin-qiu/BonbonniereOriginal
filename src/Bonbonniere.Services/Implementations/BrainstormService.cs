using Bonbonniere.Core.Models;
using Bonbonniere.Infrastructure.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bonbonniere.Services.Interfaces;

namespace Bonbonniere.Services.Implementations
{
    public class BrainstormService : IBrainstormService
    {
        private readonly IRepository<BrainstormSession> _sessionRepository;
        private readonly IUnitOfWork _uow;

        public BrainstormService(IRepository<BrainstormSession> sessionRepository, IUnitOfWork uow)
        {
            _sessionRepository = sessionRepository;
            _uow = uow;
        }

        public Task AddSessionAsync(BrainstormSession session)
        {
             _sessionRepository.Add(session);
            return _uow.CommitAsync();
        }

        public Task<BrainstormSession> GetByIdAsync(int id)
        {
            return _sessionRepository.GetAsync(t => t.Id == id, p => p.Ideas);
        }

        public Task<List<BrainstormSession>> GetListAsync()
        {
            return _sessionRepository.FetchAllAsync(p => p.Ideas);
        }

        public Task UpdateAsync(BrainstormSession session)
        {
            _sessionRepository.Update(session);
            return _uow.CommitAsync();
        }
    }
}
