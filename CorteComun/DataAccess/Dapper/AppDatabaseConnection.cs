using System;
using System.Data;
using System.Data.SqlClient;

namespace CorteComun.DataAccess.Dapper
{
    public class AppDatabaseConnection
    {
        public static readonly Lazy<IDbConnection> Instance = new Lazy<IDbConnection>(CreateConnection);

        private static IDbConnection CreateConnection()
        {
            return new SqlConnection(AppConfiguration.DataAccess.AppDatabaseConnectionString);
        }
    }
}
