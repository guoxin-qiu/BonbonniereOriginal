using Bonbonniere.Core.Sample.Model;
using Bonbonniere.Infrastructure.Data;
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
            PopulateData(_sampleContext);

            //Act
            var result = await genreMenuComponent.InvokeAsync();

            //Assert
            Assert.NotNull(result);
            var viewResult = Assert.IsType<ViewViewComponentResult>(result);
            Assert.Null(viewResult.ViewName);
            var genreResult = Assert.IsType<List<string>>(viewResult.ViewData.Model);
            Assert.Equal(8, genreResult.Count);
        }

        private static void PopulateData(SampleContext context)
        {
            var genres = Enumerable.Range(1, 9).Select(n => new Genre { Name = $"Genre - {n}" });

            context.AddRange(genres);
            context.SaveChanges();
        }
    }
}
