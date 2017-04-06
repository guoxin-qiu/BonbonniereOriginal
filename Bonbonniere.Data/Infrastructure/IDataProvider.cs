using Bonbonniere.Infrastructure;

namespace Bonbonniere.Data.Infrastructure
{
    public interface IDataProvider
    {
        DataProviderType DataProviderType { get; }
        BonbonniereContext DbContext { get; }
    }
}
