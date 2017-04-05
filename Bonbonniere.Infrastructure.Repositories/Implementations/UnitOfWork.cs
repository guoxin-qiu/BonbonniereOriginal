using Bonbonniere.Infrastructure.Domain;

namespace Bonbonniere.Infrastructure.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private BonbonniereContext _dbContext;
        public UnitOfWork(BonbonniereContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
