using Bonbonniere.Core.Interfaces;
using Bonbonniere.Data.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Bonbonniere.UnitTests.DataRepositories.MusicStore
{
    public class MusicStoreRepositoryTests : RepositoryTestBase
    {
        private readonly ITestOutputHelper _output;
        private readonly IMusicStoreRepository _storeRepo;
        public MusicStoreRepositoryTests(ITestOutputHelper output)
        {
            _output = output;
            _storeRepo = new MusicStoreRepository(_dataProvider);
        }

        [Fact]
        public async Task GetAlbumsAsync()
        {
            //Act
            var list = await _storeRepo.ListAsync();

            //Assert
            Assert.Equal(2, list.Count);
            Assert.Equal("First Album", list.First().Title);
            Assert.True(list.First().Id > 0);
            Assert.True(list.Last().Id > 0);

            _output.WriteLine($"First Album Id: {list.First().Id}");
            _output.WriteLine($"Last Album Id: {list.Last().Id}");
        }
    }
}
