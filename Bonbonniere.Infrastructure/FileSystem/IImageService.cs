namespace Bonbonniere.Infrastructure.FileSystem
{
    public interface IImageService
    {
        byte[] GetImageBytesById(string id);
    }
}
