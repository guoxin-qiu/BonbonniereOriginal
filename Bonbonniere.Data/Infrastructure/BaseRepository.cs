using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bonbonniere.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Data.Infrastructure
{
    public class BaseRepository<T> : IRepository<T>
        where T : class, IAggregateRoot
    {
        // TODO: ** Need to study deeply
        private IDataProvider _dataProvider;
        private BonbonniereContext DbContext => _dataProvider.DbContext;
        protected DbSet<T> Table => DbContext.Set<T>();

        public BaseRepository(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Add(T entity)
        {
            Table.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Table.AddRange(entities);
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            Table.UpdateRange(entities);
        }

        public void Update(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            Table.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Table.RemoveRange(entities);
        }

        public void Remove(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {

            return Table.Find(id);
        }

        public Task<T> GetByIdAsync(object id)
        {
            return Table.FindAsync(id);
        }

        public T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            return FetchAll(paths).Where(predicate).SingleOrDefault();
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            return FetchAll(paths).Where(predicate).SingleOrDefaultAsync();
        }

        public int Count()
        {
            return Table.Count();
        }

        public Task<int> CountAsync()
        {
            return Table.CountAsync();
        }

        public IQueryable<T> FetchAll(params Expression<Func<T, object>>[] paths)
        {
            var all = Table as IQueryable<T>;
            if (paths is null)
                return all;
            return paths.Aggregate(all, (current, path) => current.Include(path));
        }

        public Task<List<T>> FetchAllAsync(params Expression<Func<T, object>>[] paths)
        {
            return FetchAll(paths).ToListAsync();
        }

        public List<T> FetchAllOrdered(Action<Orderable<T>> order, params Expression<Func<T, object>>[] paths)
        {
            return FetchAllBy(order, paths).ToList();
        }

        public Task<List<T>> FetchAllOrderedAsync(Action<Orderable<T>> order, params Expression<Func<T, object>>[] paths)
        {
            return FetchAllBy(order, paths).ToListAsync();
        }

        public List<T> FetchAllOrdered(Action<Orderable<T>> order, int skip, int count, params Expression<Func<T, object>>[] paths)
        {
            return FetchAllBy(order, skip, count, paths).ToList();
        }

        public Task<List<T>> FetchAllOrderedAsync(Action<Orderable<T>> order, int skip, int count, params Expression<Func<T, object>>[] paths)
        {
            return FetchAllBy(order, skip, count, paths).ToListAsync();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return Table.Count(predicate);
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return Table.CountAsync(predicate);
        }

        public List<T> Fetch(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            return FetchBy(predicate, paths).ToList();
        }

        public Task<List<T>> FetchAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            return FetchBy(predicate, paths).ToListAsync();
        }

        public List<T> FetchOrdered(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, params Expression<Func<T, object>>[] paths)
        {
            return FetchBy(predicate, order, paths).ToList();
        }

        public Task<List<T>> FetchOrderedAsync(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, params Expression<Func<T, object>>[] paths)
        {
            return FetchBy(predicate, order, paths).ToListAsync();
        }

        public List<T> FetchOrdered(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count, params Expression<Func<T, object>>[] paths)
        {
            return FetchBy(predicate, order, skip, count, paths).ToList();
        }

        public Task<List<T>> FetchOrderedAsync(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count, params Expression<Func<T, object>>[] paths)
        {
            return FetchBy(predicate, order, skip, count, paths).ToListAsync();
        }

        #region Private
        private IQueryable<T> FetchAllBy(params Expression<Func<T, object>>[] paths)
        {
            return FetchAll(paths);
        }

        private IQueryable<T> FetchAllBy(Action<Orderable<T>> order, params Expression<Func<T, object>>[] paths)
        {
            var orderable = new Orderable<T>(FetchAll(paths));
            order(orderable);
            return orderable.Queryable;
        }

        private IQueryable<T> FetchAllBy(Action<Orderable<T>> order, int skip, int count, params Expression<Func<T, object>>[] paths)
        {
            return FetchAllBy(order, paths).Skip(skip).Take(count);
        }

        private IQueryable<T> FetchBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] paths)
        {
            return FetchAll(paths).Where(predicate);
        }

        private IQueryable<T> FetchBy(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, params Expression<Func<T, object>>[] paths)
        {
            var orderable = new Orderable<T>(FetchBy(predicate, paths));
            order(orderable);
            return orderable.Queryable;
        }

        private IQueryable<T> FetchBy(Expression<Func<T, bool>> predicate, Action<Orderable<T>> order, int skip, int count, params Expression<Func<T, object>>[] paths)
        {
            return FetchBy(predicate, order, paths).Skip(skip).Take(count);
        }
        #endregion Private
    }
}
