using Logica.Funcionalidades.Preguntas.ActualizarPregunta;
using Logica.Funcionalidades.Preguntas.BorrarPregunta;
using Logica.Funcionalidades.Preguntas.CrearPregunta;
using Logica.Funcionalidades.Preguntas.LeerPreguntas;
using Logica.Funcionalidades.Preguntas.MarcarPreguntaComoResuelta;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependenciasCapaLogica
    {
        public static IServiceCollection AgregarDependenciasCapaLogica(this IServiceCollection servicios)
        {
            servicios.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<Anchor>();
            });

            LeerPreguntasGestionDependencias.RegistrarDependencias(servicios);
            CrearPreguntaGestionDependencias.RegistrarDependencias(servicios);
            ActualizarPreguntaGestionDependencias.RegistrarDependencias(servicios);
            BorrarPreguntaGestionDependencias.RegistrarDependencias(servicios);
            MarcarPreguntaComoResueltaGestionDependencias.RegistrarDependencias(servicios);

            return servicios;
        }
    }

    internal class Anchor { }
}
