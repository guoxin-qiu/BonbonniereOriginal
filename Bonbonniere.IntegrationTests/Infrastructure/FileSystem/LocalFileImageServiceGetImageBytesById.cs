using Bonbonniere.Infrastructure.FileSystem;
using Microsoft.AspNetCore.Hosting;
using Moq;
using System.IO;
using Xunit;

namespace Bonbonniere.IntegrationTests.Infrastructure.FileSystem
{
    public class LocalFileImageServiceGetImageBytesById
    {
        private byte[] _testBytes = new byte[] { 0x01, 0x02, 0x03 };
        private readonly Mock<IHostingEnvironment> _mockEnvironment = new Mock<IHostingEnvironment>();
        private string _testImageId = "dotnetcup";
        private string _testFileName = "dotnetcup.png";

        public LocalFileImageServiceGetImageBytesById()
        {
            // create folder if necessary
            Directory.CreateDirectory(Path.Combine(GetFileDirectory(), "wwwroot", "images"));

            string filePath = GetFilePath(_testFileName);
            File.WriteAllBytes(filePath, _testBytes);
            _mockEnvironment.SetupGet(m => m.ContentRootPath).Returns(GetFileDirectory());
        }

        private string GetFilePath(string fileName)
        {
            return Path.Combine(GetFileDirectory(), "wwwroot", "images", fileName);
        }

        private string GetFileDirectory()
        {
            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            return Path.GetDirectoryName(location);
        }

        [Fact]
        public void ReturnsFileContentResultGivenValidId()
        {
            var fileService = new LocalFileImageService(_mockEnvironment.Object);

            var result = fileService.GetImageBytesById(_testImageId);

            Assert.Equal(_testBytes, result);
        }
    }
}
