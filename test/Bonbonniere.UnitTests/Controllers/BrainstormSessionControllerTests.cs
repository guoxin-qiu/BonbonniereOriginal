using Bonbonniere.Core.Models;
using Bonbonniere.Services.Interfaces;
using Bonbonniere.Website.Features.BrainstormSession;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Bonbonniere.UnitTests.Controllers
{
    public class BrainstormSessionControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        {
            // Arrange
            var mockService = new Mock<IBrainstormService>();
            mockService.Setup(srv => srv.GetListAsync()).Returns(Task.FromResult(GetTestSessions()));
            var controller = new BrainstormSessionController(mockService.Object);

            //Act
            var result = await controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<BrainstormSessionViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task IndexPost_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockService = new Mock<IBrainstormService>();
            var controller = new BrainstormSessionController(mockService.Object);
            controller.ModelState.AddModelError("SessionName", "Required");
            var newSession = new BrainstormSessionController.NewSessionModel();

            //Act
            var result = await controller.Index(newSession);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SessionName_Required()
        {
            //Arrange
            var model = new BrainstormSessionController.NewSessionModel()
            {
                SessionName = null
            };
            var context = new ValidationContext(model);
            var result = new List<ValidationResult>();

            //Act
            var valid = Validator.TryValidateObject(model, context, result, true);

            //Assert
            Assert.False(valid);
            var failure = Assert.Single(result, x => x.ErrorMessage == "The SessionName field is required.");
            Assert.Single(failure.MemberNames, x => x == "SessionName");
        }

        [Fact]
        public async Task IndexPost_ReturnsARedirectToIndexAndAddsSession_WhenModelStateIsValid()
        {
            // Arrange
            var mockService = new Mock<IBrainstormService>();
            mockService.Setup(srv => srv.AddSessionAsync(It.IsAny<BrainstormSession>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            var controller = new BrainstormSessionController(mockService.Object);
            var newSession = new BrainstormSessionController.NewSessionModel
            {
                SessionName = "Test Name"
            };

            //Act
            var result = await controller.Index(newSession);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockService.Verify();//will fail the test if the expected method was not called
        }

        [Fact]
        public async Task Details_ReturnsARedirectToIndex_WhenIdIsNull()
        {
            //Arrange
            var controller = new BrainstormSessionController(null);

            //Act
            var result = await controller.Details(id: null);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Details_ReturnsContentWithSessionNotFound_WhenSessionNotFound()
        {
            //Arrange
            int testSessionId = 1;
            var mockService = new Mock<IBrainstormService>();
            mockService.Setup(srv => srv.GetByIdAsync(testSessionId))
                .Returns(Task.FromResult((BrainstormSession)null));
            var controller = new BrainstormSessionController(mockService.Object);

            //Act
            var result = await controller.Details(testSessionId);

            //Assert
            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal("Session not found.", contentResult.Content);
        }

        [Fact]
        public async Task Detail_ReturnsViewResultWithBrainstormSessionViewModel_WhenSessionFound()
        {
            //Arrange
            int testSessionId = 1;
            var mockService = new Mock<IBrainstormService>();
            mockService.Setup(srv => srv.GetByIdAsync(testSessionId))
                .Returns(Task.FromResult(GetTestSessions().FirstOrDefault(s => s.Id == testSessionId)));
            var controller = new BrainstormSessionController(mockService.Object);

            //Act
            var result = await controller.Details(testSessionId);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<BrainstormSessionViewModel>(viewResult.ViewData.Model);
            Assert.Equal("Test One", model.Name);
            Assert.Equal(11, model.DateCreated.Day);
            Assert.Equal(testSessionId, model.Id);
        }

        private List<BrainstormSession> GetTestSessions()
        {
            var session = new List<BrainstormSession>();
            session.Add(new BrainstormSession
            {
                DateCreated = new DateTime(2017, 4, 11),
                Id = 1,
                Name = "Test One"
            });
            session.Add(new BrainstormSession
            {
                DateCreated = new DateTime(2017, 4, 10),
                Id = 2,
                Name = "Test Two"
            });

            return session;
        }
    }
}
