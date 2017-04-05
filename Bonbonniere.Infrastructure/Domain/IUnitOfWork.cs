namespace Bonbonniere.Infrastructure.Domain
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
