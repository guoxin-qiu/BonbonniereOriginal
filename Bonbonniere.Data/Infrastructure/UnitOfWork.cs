using Bonbonniere.Infrastructure.Domain;

namespace Bonbonniere.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDataProvider _dataProvider;

        public UnitOfWork(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void Commit()
        {
            _dataProvider.DbContext.SaveChanges();
        }
    }
}
