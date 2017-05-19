using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Bonbonniere.FunctionalTests
{
    public class HomeControllerGetImageTests : BaseWebTest
    {
        [Fact]
        public async Task ReturnsFileContentResultGivenValidId()
        {
            var testFilePath = Path.Combine(_contentRoot, "wwwroot", "images", "dotnetcup.png");
            var expectedFileBytes = File.ReadAllBytes(testFilePath);

            var response = await _client.GetAsync("/home/images/dotnetcup");
            response.EnsureSuccessStatusCode();
            var streamResponse = await response.Content.ReadAsStreamAsync();
            byte[] byteResult;
            using (var ms = new MemoryStream())
            {
                streamResponse.CopyTo(ms);
                byteResult = ms.ToArray();
            }

            Assert.Equal(expectedFileBytes, byteResult);
        }
    }
}
