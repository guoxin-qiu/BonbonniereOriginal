namespace Bonbonniere.Infrastructure.Domain
{
    public interface IRepository<T>
    {
        T GetById(object id);
        void Save(T entity);
    }
}
