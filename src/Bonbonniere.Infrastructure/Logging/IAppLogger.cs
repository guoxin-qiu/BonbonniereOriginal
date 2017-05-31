namespace Bonbonniere.Infrastructure.Logging
{
    public interface IAppLogger<T>
    {
        void LogWarning(string message, params object[] args);
    }
}
