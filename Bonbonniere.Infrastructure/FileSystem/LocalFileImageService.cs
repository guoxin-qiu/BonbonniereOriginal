using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Bonbonniere.Infrastructure.FileSystem
{
    public class LocalFileImageService : IImageService
    {
        private readonly IHostingEnvironment _env;

        public LocalFileImageService(IHostingEnvironment env)
        {
            _env = env;
        }
        public byte[] GetImageBytesById(string id)
        {
            try
            {
                var contentRoot = _env.ContentRootPath + "//wwwroot//images";
                var path = Path.Combine(contentRoot, id + ".png");
                return File.ReadAllBytes(path);
            }
            catch (FileNotFoundException ex)
            {
                throw new CatalogImageMissingException(ex);
            }
        }
    }
}
