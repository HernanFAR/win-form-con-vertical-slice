using CorteComun.DataAccess.EntityFramework;
using CorteComun.Funcional.Resultados;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dominio.Funcional.Resultados;

// ReSharper disable once CheckNamespace
namespace Logica.Funcionalidades.Preguntas.ActualizarPregunta
{
    public class ActualizarPreguntaGestionDependencias
    {
        public static IServiceCollection RegistrarDependencias(IServiceCollection servicios)
        {
            servicios.AddTransient<IActualizarPreguntaRepositorio, ActualizarPreguntaRepositorio>();
            servicios.AddTransient<IValidator<ActualizarPreguntaComando>, ActualizarPreguntaValidador>();

            return servicios;
        }
    }

    public class ActualizarPreguntaComando : IRequest<Respuesta<Exito>>
    {
        public Guid Id { get; }

        public string Titulo { get; set; }

        public string Detalle { get; set; }

        public ActualizarPreguntaComando(Guid Id)
        {
            this.Id = Id;
        }

    }

    public class ActualizarPreguntaValidador : AbstractValidator<ActualizarPreguntaComando>
    {
        private readonly IActualizarPreguntaRepositorio _repositorio;

        public ActualizarPreguntaValidador(IActualizarPreguntaRepositorio repositorio)
        {
            _repositorio = repositorio;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("El id no puede estar vacio");

            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El titulo no puede estar vacio")
                .MinimumLength(3).WithMessage("Debes incluir minimo 3 caracteres")
                .MustAsync(TituloNoExiste);

            When(e => !string.IsNullOrEmpty(e.Detalle),
                () =>
                {
                    RuleFor(x => x.Detalle)
                        .MinimumLength(5).WithMessage("Debes incluir minimo 5 caracteres");
                });
        }

        private Task<bool> TituloNoExiste(ActualizarPreguntaComando comando, string titulo, CancellationToken token)
        {
            return _repositorio.TituloNoExiste(comando.Id, comando.Titulo, token);
        }
    }

    public class ActualizarPreguntaManejador : IRequestHandler<ActualizarPreguntaComando, Respuesta<Exito>>
    {
        private readonly IActualizarPreguntaRepositorio _repositorio;
        private readonly IValidator<ActualizarPreguntaComando> _validador;

        public ActualizarPreguntaManejador(IActualizarPreguntaRepositorio repositorio, IValidator<ActualizarPreguntaComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Respuesta<Exito>> Handle(ActualizarPreguntaComando request, CancellationToken cancellationToken)
        {
            var validacion = await _validador.ValidateAsync(request, cancellationToken);

            if (!validacion.IsValid)
            {
                return new ErrorDeNegocio(
                    TipoDeError.ErrorDeValidation,
                    "Hay errores de validación en el formulario",
                    Array.Empty<MensajeDeValidacion>());
            }

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
                    "No puedes editar una pregunta que ya ha sido respondida"
                );
            }

            pregunta.Editar(request.Titulo, request.Detalle);

            return await _repositorio.Actualizar(pregunta, cancellationToken);
        }
    }

    public interface IActualizarPreguntaRepositorio
    {
        Task<Respuesta<Pregunta>> Buscar(Guid id, CancellationToken cancellationToken);

        Task<Respuesta<Exito>> Actualizar(Pregunta pregunta, CancellationToken cancellationToken);

        Task<bool> TituloNoExiste(Guid id, string titulo, CancellationToken cancellationToken);
    }

    public class ActualizarPreguntaRepositorio : IActualizarPreguntaRepositorio
    {
        private readonly AppDbContext _context;

        public ActualizarPreguntaRepositorio(AppDbContext context)
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

        public async Task<Respuesta<Exito>> Actualizar(Pregunta pregunta, CancellationToken cancellationToken)
        {
            _context.Entry(pregunta).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);

            return Exito.Valor;
        }

        public Task<bool> TituloNoExiste(Guid id, string titulo, CancellationToken cancellationToken)
        {
            return _context.Preguntas
                .Where(x => x.Id != id)
                .Where(x => x.Titulo != titulo)
                .AnyAsync(cancellationToken);
        }
    }
}
