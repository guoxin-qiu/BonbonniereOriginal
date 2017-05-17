using Bonbonniere.Core.Models.MusicStore;
using Bonbonniere.Data.Infrastructure;
using Bonbonniere.Website.Additions.Components;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Bonbonniere.IntegrationTests
{
    public class GenreMenuComponentTests : IntegrationTestBase
    {
        public GenreMenuComponentTests(ITestOutputHelper output)
            : base(output)
        {
        }

        [Fact]
        public async Task GenreMenuComponent_Returns_NineGenres()
        {
            var genreMenuComponent = _serviceProvider.GetRequiredService<GenreMenuViewComponent>();
            PopulateData(_dbContext);

            //Act
            var result = await genreMenuComponent.InvokeAsync();

            //Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewViewComponentResult>(result);
            Assert.Null(viewResult.ViewName);
            var genreResult = Assert.IsType<List<string>>(viewResult.ViewData.Model);
            Assert.Equal(7, genreResult.Count);
        }

        private static void PopulateData(BonbonniereContext context)
        {
            var genres = Enumerable.Range(1, 7).Select(n => new Genre { Id = n, Name = $"Genre - {n}" });

            context.AddRange(genres);
            context.SaveChanges();
        }
    }
}
