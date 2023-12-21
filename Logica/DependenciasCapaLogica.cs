using Logica.Funcionalidades.Preguntas.CrearPregunta;
using Logica.Funcionalidades.Preguntas.LeerPreguntas;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependenciasCapaLogica
    {
        public static IServiceCollection AgregarDependenciasCapaLogica(this IServiceCollection servicios)
        {
            servicios.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssemblyContaining<Anchor>();
            });

            LeerPreguntasGestionDependencias.RegistrarDependencias(servicios);
            CrearPreguntaGestionDependencias.RegistrarDependencias(servicios);

            return servicios;
        }
    }

    internal class Anchor {}
}
