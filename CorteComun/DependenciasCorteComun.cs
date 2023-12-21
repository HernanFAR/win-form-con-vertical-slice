using System.Data;
using CorteComun.DataAccess.Dapper;
using CorteComun.DataAccess.EntityFramework;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependenciasCorteComun
    {
        public static IServiceCollection AgregarDependenciasCorteComun(this IServiceCollection servicios)
        {
            return servicios.AddTransient<AppDbContext>()
                .AddTransient(_ => AppDatabaseConnection.Instance.Value);
        }
    }
}
