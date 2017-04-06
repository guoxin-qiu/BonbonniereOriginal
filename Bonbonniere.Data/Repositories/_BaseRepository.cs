using Bonbonniere.Data.Infrastructure;
using Bonbonniere.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Data.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
    {
        private IDataProvider _dataProvider;
        private BonbonniereContext DbContext => _dataProvider.DbContext;
        private DbSet<TEntity> Table => DbContext.Set<TEntity>();

        public BaseRepository(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public TEntity GetById(object id)
        {
            //TODO: what's the differences?
            //return DbContext.Find<TEntity>(id);
            return Table.Find(id);
        }

        public void Save(TEntity entity)
        {
            Table.Add(entity);
        }
    }
}
