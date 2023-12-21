using CorteComun.Funcional.Resultados;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Logica.Funcionalidades.Preguntas.LeerPreguntas
{
    public class LeerPreguntasGestionDependencias
    {
        public static IServiceCollection RegistrarDependencias(IServiceCollection servicios)
        {
            servicios.AddSingleton<ILeerPreguntasRepositorio, LeerPreguntasRepositorio>();

            return servicios;
        }
    }

    public class PreguntaDTO
    {
        public PreguntaDTO(Guid id, string titulo, string detalle)
        {
            Id = id;
            Titulo = titulo;
            Detalle = detalle;
        }

        public Guid Id { get; }

        public string Titulo { get; }

        public string Detalle { get; }

    }

    public class LeerPreguntasDTO
    {
        public PreguntaDTO[] Preguntas { get; }

        public LeerPreguntasDTO(PreguntaDTO[] preguntas)
        {
            Preguntas = preguntas;
        }

    }

    public class LeerPreguntasConsulta : IRequest<Respuesta<LeerPreguntasDTO>>
    {
        public static readonly LeerPreguntasConsulta Instancia = new LeerPreguntasConsulta();
    }

    public class LeerPreguntasManejador : IRequestHandler<LeerPreguntasConsulta, Respuesta<LeerPreguntasDTO>>
    {
        private readonly ILeerPreguntasRepositorio _repositorio;

        public LeerPreguntasManejador(ILeerPreguntasRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Respuesta<LeerPreguntasDTO>> Handle(LeerPreguntasConsulta request, CancellationToken cancellationToken)
        {
            return await _repositorio.LeerTodas(cancellationToken);
        }
    }

    public interface ILeerPreguntasRepositorio
    {
        Task<Respuesta<LeerPreguntasDTO>> LeerTodas(CancellationToken cancellationToken);
    }

    public class LeerPreguntasRepositorio : ILeerPreguntasRepositorio
    {
        public async Task<Respuesta<LeerPreguntasDTO>> LeerTodas(CancellationToken cancellationToken)
        {
            return new LeerPreguntasDTO(new[]
            {
                new PreguntaDTO(Guid.NewGuid(), "Titulo 1", "Detalle 1"),
                new PreguntaDTO(Guid.NewGuid(), "Titulo 2", "Detalle 2"),
                new PreguntaDTO(Guid.NewGuid(), "Titulo 3", "Detalle 3"),
                new PreguntaDTO(Guid.NewGuid(), "Titulo 4", "Detalle 4"),
                new PreguntaDTO(Guid.NewGuid(), "Titulo 5", "Detalle 5"),
            });
        }
    }
}
