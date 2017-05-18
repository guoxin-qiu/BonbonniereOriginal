using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bonbonniere.Infrastructure.Domain
{
    public interface IRepository<T> : IReadonlyRepository<T> where T: IAggregateRoot
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Update(Expression<Func<T, bool>> predicate);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Remove(Expression<Func<T, bool>> predicate);
    }
}
