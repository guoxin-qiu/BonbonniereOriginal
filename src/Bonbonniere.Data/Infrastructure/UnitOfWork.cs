using Bonbonniere.Infrastructure.Domain;
using System.Threading.Tasks;

namespace Bonbonniere.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        // TODO: Transaction
        private IDataProvider _dataProvider;

        public UnitOfWork(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Commit()
        {
            _dataProvider.DbContext.SaveChanges();
        }

        public Task CommitAsync()
        {
            return _dataProvider.DbContext.SaveChangesAsync();
        }
    }
}
