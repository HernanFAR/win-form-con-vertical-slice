using Microsoft.Extensions.DependencyInjection;
using System;

namespace WinFormsSample.Herramientas
{
    public class IniciadorServiceProvider
    {
        public static readonly Lazy<IServiceProvider> ProveedorLazy = new Lazy<IServiceProvider>(
            () => new ServiceCollection()
            .AgregarDependenciasCapaLogica()
            .AgregarDependenciasCorteComun()
            .BuildServiceProvider());

    }
}
