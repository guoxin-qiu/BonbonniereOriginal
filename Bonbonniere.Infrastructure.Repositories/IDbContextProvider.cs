using Microsoft.EntityFrameworkCore;

namespace Bonbonniere.Infrastructure.Repositories
{
    public interface IDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {
        TDbContext DbContext { get; }
    }
}
