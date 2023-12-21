using CorteComun.DataAccess.EntityFramework;
using CorteComun.Funcional.Resultados;
using Dominio;
using Dominio.Funcional.Resultados;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Logica.Funcionalidades.Preguntas.MarcarPreguntaComoResuelta
{
    public class MarcarPreguntaComoResueltaGestionDependencias
    {
        public static IServiceCollection RegistrarDependencias(IServiceCollection servicios)
        {
            servicios.AddTransient<IMarcarPreguntaComoResueltaRepositorio, MarcarPreguntaComoResueltaRepositorio>();

            return servicios;
        }
    }

    public class MarcarPreguntaComoResueltaComando : IRequest<Respuesta<Exito>>
    {
        public Guid Id { get; }

        public MarcarPreguntaComoResueltaComando(Guid Id)
        {
            this.Id = Id;
        }
    }

    public class MarcarPreguntaComoResueltaManejador : IRequestHandler<MarcarPreguntaComoResueltaComando, Respuesta<Exito>>
    {
        private readonly IMarcarPreguntaComoResueltaRepositorio _repositorio;

        public MarcarPreguntaComoResueltaManejador(IMarcarPreguntaComoResueltaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Respuesta<Exito>> Handle(MarcarPreguntaComoResueltaComando request, CancellationToken cancellationToken)
        {
            var respuesta = await _repositorio.Buscar(request.Id, cancellationToken);

            if (respuesta.ConError)
            {
                return respuesta.ErrorDeNegocio;
            }

            var pregunta = respuesta.Valor;

            if (pregunta.Respondida)
            {
                return new ErrorDeNegocio(
                    TipoDeError.ErrorDeLogica,
                    "No puedes resolver una pregunta que ya ha sido resuelta"
                );
            }

            pregunta.Resolver();

            return await _repositorio.Actualizar(pregunta, cancellationToken);
        }
    }

    public interface IMarcarPreguntaComoResueltaRepositorio
    {
        Task<Respuesta<Pregunta>> Buscar(Guid id, CancellationToken cancellationToken);

        Task<Respuesta<Exito>> Actualizar(Pregunta pregunta, CancellationToken cancellationToken);
    }

    public class MarcarPreguntaComoResueltaRepositorio : IMarcarPreguntaComoResueltaRepositorio
    {
        private readonly AppDbContext _context;

        public MarcarPreguntaComoResueltaRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Respuesta<Pregunta>> Buscar(Guid id, CancellationToken cancellationToken)
        {
            var pregunta = await _context.Preguntas
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync(cancellationToken);

            if (pregunta == null) return new ErrorDeNegocio(TipoDeError.RecursoNoEncontrado);

            return pregunta;
        }

        public async Task<Respuesta<Exito>> Actualizar(Pregunta pregunta, CancellationToken cancellationToken)
        {
            _context.Entry(pregunta).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);

            return Exito.Valor;
        }
    }
}
