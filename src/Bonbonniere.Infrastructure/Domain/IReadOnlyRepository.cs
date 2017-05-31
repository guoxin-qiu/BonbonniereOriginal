using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bonbonniere.Infrastructure.Domain
{
    public interface IReadonlyRepository<T> where T: IAggregateRoot
    {
        T GetById(object id);
        Task<T> GetByIdAsync(object id);
        T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);

        int Count();
        Task<int> CountAsync();
        List<T> FetchAll(params Expression<Func<T, object>>[] paths);
        Task<List<T>> FetchAllAsync(params Expression<Func<T, object>>[] paths);
        List<T> FetchAllOrdered(Action<Orderable<T>> order, params Expression<Func<T, object>>[] paths);
        Task<List<T>> FetchAllOrderedAsync(Action<Orderable<T>> order, params Expression<Func<T, object>>[] paths);
        List<T> FetchAllOrdered(Action<Orderable<T>> order, int skip, int count, params Expression<Func<T, object>>[] paths);
        Task<List<T>> FetchAllOrderedAsync(Action<Orderable<T>> order, int skip, int count, params Expression<Func<T, object>>[] paths);

        int Count(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        List<T> Fetch(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);
        Task<List<T>> FetchAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths);
        List<T> FetchOrdered(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, params Expression<Func<T, object>>[] paths);
        Task<List<T>> FetchOrderedAsync(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, params Expression<Func<T, object>>[] paths);
        List<T> FetchOrdered(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count, params Expression<Func<T, object>>[] paths);
        Task<List<T>> FetchOrderedAsync(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count, params Expression<Func<T, object>>[] paths);
    }
}
