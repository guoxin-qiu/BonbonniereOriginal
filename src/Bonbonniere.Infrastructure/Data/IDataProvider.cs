using Bonbonniere.Infrastructure.Environment;

namespace Bonbonniere.Infrastructure.Data
{
    public interface IDataProvider
    {
        DataProviderType DataProviderType { get; }
        BonbonniereContext DbContext { get; }
    }
}
