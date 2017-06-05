using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Bonbonniere.Website.Features.Samples
{
    public class FileProviderSampleController : Controller
    {
        private readonly IHostingEnvironment _env;
        public FileProviderSampleController(IHostingEnvironment env)
        {
            _env = env;
        }

        public IActionResult PhysicalFiles()
        {
            var contents = _env.ContentRootFileProvider.GetDirectoryContents("");
            return View("Index", contents);
        }

        public IActionResult EmbeddedFiles()
        {
            var contents = new EmbeddedFileProvider(Assembly.GetEntryAssembly()).GetDirectoryContents("");
            return View("Index", contents);
        }

        public IActionResult CompositeFiles()
        {
            var physicalProvider = _env.ContentRootFileProvider;
            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetEntryAssembly());
            var compositeProvider = new CompositeFileProvider(physicalProvider, embeddedProvider);

            var contents = compositeProvider.GetDirectoryContents("");
            return View("Index", contents);
        }
    }
}
