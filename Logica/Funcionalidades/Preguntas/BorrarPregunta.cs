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
namespace Logica.Funcionalidades.Preguntas.BorrarPregunta
{
    public class BorrarPreguntaGestionDependencias
    {
        public static IServiceCollection RegistrarDependencias(IServiceCollection servicios)
        {
            servicios.AddTransient<IBorrarPreguntaRepositorio, BorrarPreguntaRepositorio>();

            return servicios;
        }
    }

    public class BorrarPreguntaComando : IRequest<Respuesta<Exito>>
    {
        public Guid Id { get; }

        public BorrarPreguntaComando(Guid Id)
        {
            this.Id = Id;
        }
    }

    public class BorrarPreguntaManejador : IRequestHandler<BorrarPreguntaComando, Respuesta<Exito>>
    {
        private readonly IBorrarPreguntaRepositorio _repositorio;

        public BorrarPreguntaManejador(IBorrarPreguntaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Respuesta<Exito>> Handle(BorrarPreguntaComando request, CancellationToken cancellationToken)
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
                    "No puedes borrar una pregunta que ya ha sido respondida"
                );
            }

            return await _repositorio.Borrar(pregunta, cancellationToken);
        }
    }

    public interface IBorrarPreguntaRepositorio
    {
        Task<Respuesta<Pregunta>> Buscar(Guid id, CancellationToken cancellationToken);

        Task<Respuesta<Exito>> Borrar(Pregunta pregunta, CancellationToken cancellationToken);
    }

    public class BorrarPreguntaRepositorio : IBorrarPreguntaRepositorio
    {
        private readonly AppDbContext _context;

        public BorrarPreguntaRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Respuesta<Pregunta>> Buscar(Guid id, CancellationToken cancellationToken)
        {
            var anon = await _context.Preguntas
                .Where(x => x.Id == id)
                .Select(x => new { x.Id, x.Titulo, x.Detalle })
                .SingleOrDefaultAsync(cancellationToken);

            if (anon == null) return new ErrorDeNegocio(TipoDeError.RecursoNoEncontrado);

            return new Pregunta(anon.Id, anon.Titulo, anon.Detalle);
        }

        public async Task<Respuesta<Exito>> Borrar(Pregunta pregunta, CancellationToken cancellationToken)
        {
            _context.Entry(pregunta).State = EntityState.Deleted;

            await _context.SaveChangesAsync(cancellationToken);

            return Exito.Valor;
        }
    }
}
