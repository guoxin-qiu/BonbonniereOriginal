using Bonbonniere.Website;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Bonbonniere.IntegrationTests
{
    public class BrainstormSessionControllerTests: IClassFixture<TestFixture<Startup>>
    {
        private readonly HttpClient _client;
        private ITestOutputHelper _output;

        public BrainstormSessionControllerTests(TestFixture<Startup> fixture, ITestOutputHelper output)
        {
            _client = fixture.Client;
            _output = output;
        }

        [Fact]
        public async Task IndexReturnsCorrentSessionPage()
        {
            //Arrange
            var testSession = Startup.GetTestSession();

            //Arrange & Act
            var response = await _client.GetAsync("/BrainstormSession/Index");

            //Assert
            _output.WriteLine("Return ViewResult is not correct.");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.True(responseString.Contains(testSession.Name));
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
            // Arrange
            var testSession = Startup.GetTestSession();

            // Arrange & Act
            var response = await _client.GetAsync("/BrainstormSession/Details/1");
            _output.WriteLine("Return ViewResult is not correct.");
            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.True(responseString.Contains(testSession.Name));
        }
    }
}
