using System.Configuration;

namespace CorteComun
{
    public static class AppConfiguration
    {
        public static class DataAccess
        {
            public static readonly string AppDatabaseKey = "AppDatabase";
            public static readonly string AppDatabaseConnectionString = ConfigurationManager.ConnectionStrings[AppDatabaseKey].ConnectionString;
        }
    }
}
