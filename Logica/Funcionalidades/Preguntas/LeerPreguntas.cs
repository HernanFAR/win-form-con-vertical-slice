using CorteComun.Funcional.Resultados;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

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
        public Guid Id { get; private set; }

        public string Titulo { get; private set;}

        public string Detalle { get; private set;}

        public bool Resuelta { get; private set;}
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
        private readonly IDbConnection _connection;

        public LeerPreguntasRepositorio(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<Respuesta<LeerPreguntasDTO>> LeerTodas(CancellationToken cancellationToken)
        {
            var preguntas = await _connection.QueryAsync<PreguntaDTO>("SELECT Id,Titulo,Detalle,Respondida FROM Preguntas");

            return new LeerPreguntasDTO(preguntas.ToArray());
        }
    }
}
