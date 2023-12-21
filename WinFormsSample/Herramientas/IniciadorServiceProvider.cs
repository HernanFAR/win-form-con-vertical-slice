using Microsoft.Extensions.DependencyInjection;
using System;

namespace WinFormsSample.Herramientas
{
    public class IniciadorServiceProvider
    {
        public static readonly Lazy<IServiceProvider> ProveedorLazy = new Lazy<IServiceProvider>(() =>
        {
            var servicios = new ServiceCollection();

            servicios.AgregarDependenciasCapaLogica();

            return servicios.BuildServiceProvider();
        });

    }
}
