using System.Threading.Tasks;

namespace Bonbonniere.Infrastructure.Domain
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}
