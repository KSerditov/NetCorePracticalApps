namespace WorkingWithEFCore
{
    public static class ProjectConstants
    {
        public const DBProvider configuredDb = DBProvider.SQLServer;
        public enum DBProvider
        {
            SQLite,
            SQLServer
        }
    }
}