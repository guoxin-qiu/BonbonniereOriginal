using Bonbonniere.Core.Interfaces;
using Bonbonniere.Core.Models.MusicStore;
using Bonbonniere.Website.Areas.MusicStore.Controllers;
using Bonbonniere.Website.Areas.MusicStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Bonbonniere.UnitTests.Controllers.MusicStore
{
    public class StoreManagerControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfAlbums()
        {
            //Arrange
            var mockRepo = new Mock<IMusicStoreRepository>();
            mockRepo.Setup(repo => repo.ListAsync())
                .Returns(Task.FromResult(GetTestAlbums()));
            var controller = new StoreManagerController(mockRepo.Object);

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
