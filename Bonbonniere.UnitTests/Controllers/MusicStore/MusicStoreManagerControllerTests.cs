using Bonbonniere.Core.Models.MusicStore;
using Bonbonniere.Services;
using Bonbonniere.Website.Features.MusicStore;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Bonbonniere.UnitTests.Controllers.MusicStore
{
    public class MusicStoreManagerControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfAlbums()
        {
            //Arrange
            var mockRepo = new Mock<IMusicStoreService>();
            mockRepo.Setup(repo => repo.GetListAsync())
                .Returns(Task.FromResult(GetTestAlbums()));
            var controller = new MusicStoreManagerController(mockRepo.Object);

            //Act
            var result = await controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<AlbumsViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private List<Album> GetTestAlbums()
        {
            return new List<Album>
            {
                new Album{Id = 1, Title = "First Test Album", Price = 9.9M},
                new Album{Id = 2, Title = "Second Test Album", Price = 16.6M}
            };
        }
    }
}
