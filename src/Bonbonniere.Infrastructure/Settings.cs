namespace Bonbonniere.Infrastructure
{
    public class Settings
    {
        public string DefaultConnection { get; set; }
        public DataProviderType DataProvider { get; set; }
    }

    public enum DataProviderType
    {
        InMemory,
        MSSQL,
        SQLite,
    }
}
