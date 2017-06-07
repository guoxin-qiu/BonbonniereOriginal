using Bonbonniere.Core.Models.MusicStore;
using Bonbonniere.Infrastructure.Paging;
using Bonbonniere.Services.Interfaces;
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
        public void Index_ReturnsAViewResult_WithAListOfAlbums()
        {
            //Arrange
            var mockRepo = new Mock<IMusicStoreService>();
            mockRepo.Setup(repo => repo.GetPagedList("","",1,3))
                .Returns(PaginatedList<Album>.Create(GetTestAlbums(),1,3));
            var controller = new MusicStoreManagerController(mockRepo.Object);

            //Act
            var result = controller.Index("", "", "", null);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PaginatedList<AlbumsViewModel>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private IQueryable<Album> GetTestAlbums()
        {
            return new List<Album>
            {
                new Album{Id = 1, Title = "First Test Album", Price = 9.9M},
                new Album{Id = 2, Title = "Second Test Album", Price = 16.6M}
            }.AsQueryable();
        }
    }
}
