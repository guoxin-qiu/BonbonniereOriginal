using Bonbonniere.Infrastructure.Data;

namespace Bonbonniere.Services.Implementations
{
    public class ServiceBase
    {
        protected readonly BonbonniereContext _context;

        public ServiceBase(IDataProvider dataProvider)
        {
            _context = dataProvider.DbContext;
        }
    }
}
