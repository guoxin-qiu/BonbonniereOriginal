using System;
using Bonbonniere.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Infrastructure.Repositories.Implementations
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private BonbonniereContext _dbContext;
        protected DbSet<TEntity> Table => _dbContext.Set<TEntity>();

        public RepositoryBase(BonbonniereContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TEntity GetById(object id)
        {
            return _dbContext.Find<TEntity>(id);
        }

        public void Save(TEntity entity)
        {
            _dbContext.Add(entity);
        }
    }
}
