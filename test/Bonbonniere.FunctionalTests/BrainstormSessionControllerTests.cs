using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Bonbonniere.FunctionalTests
{
    public class BrainstormSessionControllerTests : BaseWebTest
    {
        [Fact]
        public async Task IndexReturnsCorrentSessionPage()
        {
            //Arrange
      
            //Arrange & Act
            var response = await _client.GetAsync("/BrainstormSession/Index");

            //Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("<title>Brainstormer - Bonbonniere</title>", responseString);
        }

        [Fact]
        public async Task PostAddsNewBrainstormSession()
        {
            // Arrange
            string testSessionName = Guid.NewGuid().ToString();
            var data = new Dictionary<string, string>
            {
                { "SessionName", testSessionName }
            };
            var content = new FormUrlEncodedContent(data);

            // Act
            var response = await _client.PostAsync("/BrainstormSession/Index", content);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/BrainstormSession", response.Headers.Location.ToString());
        }

        [Fact]
        public async Task DetailsReturnsCorrectSessionPage()
        {
            // Arrange & Act
            var response = await _client.GetAsync("/BrainstormSession/Details/999");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Equal("Session not found.", responseString);
        }
    }
}
