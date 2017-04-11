﻿using Bonbonniere.Core.Interfaces;
using Bonbonniere.Core.Models;
using Bonbonniere.Website.Controllers;
using Bonbonniere.Website.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
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
            var mockReop = new Mock<IBrainstormSessionRepository>();
            mockReop.Setup(repo => repo.ListAsync()).Returns(Task.FromResult(GetTestSessions()));
            var controller = new BrainstormSessionController(mockReop.Object);

            //Act
            var result = await controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<BrainstormSessionViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task IndexPost_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockReop = new Mock<IBrainstormSessionRepository>();
            var controller = new BrainstormSessionController(mockReop.Object);
            controller.ModelState.AddModelError("SessionName", "Required");
            var newSession = new BrainstormSessionController.NewSessionModel();

            //Act
            var result = await controller.Index(newSession);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task IndexPost_ReturnsARedirectToIndexAndAddsSession_WhenModelStateIsValid()
        {
            // Arrange
            var mockReop = new Mock<IBrainstormSessionRepository>();
            mockReop.Setup(repo => repo.AddAsync(It.IsAny<BrainstormSession>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            var controller = new BrainstormSessionController(mockReop.Object);
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
            mockReop.Verify();//will fail the test if the expected method was not called
        }

        [Fact]
        public async Task Details_ReturnsARedirectToIndex_WhenIdIsNull()
        {
            //Arrange
            var controller = new BrainstormSessionController(sessionRepository: null);

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
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
                .Returns(Task.FromResult((BrainstormSession)null));
            var controller = new BrainstormSessionController(mockRepo.Object);

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
            var mockRepo = new Mock<IBrainstormSessionRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
                .Returns(Task.FromResult(GetTestSessions().FirstOrDefault(s => s.Id == testSessionId)));
            var controller = new BrainstormSessionController(mockRepo.Object);

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