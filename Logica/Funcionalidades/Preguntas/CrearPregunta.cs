using CorteComun.DataAccess.EntityFramework;
using CorteComun.Funcional.Resultados;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace Logica.Funcionalidades.Preguntas.CrearPregunta
{
    public class CrearPreguntaGestionDependencias
    {
        public static IServiceCollection RegistrarDependencias(IServiceCollection servicios)
        {
            servicios.AddTransient<ICrearPreguntaRepositorio, CrearPreguntaRepositorio>();
            servicios.AddTransient<IValidator<CrearPreguntaComando>, CrearPreguntaValidador>();

            return servicios;
        }
    }

    public class CrearPreguntaComando : IRequest<Respuesta<Exito>>
    {
        public string Titulo { get; set; }

        public string Detalle { get; set; }

    }

    public class CrearPreguntaValidador : AbstractValidator<CrearPreguntaComando>
    {
        private readonly ICrearPreguntaRepositorio _repositorio;

        public CrearPreguntaValidador(ICrearPreguntaRepositorio repositorio)
        {
            _repositorio = repositorio;

            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El titulo no puede estar vacio")
                .MinimumLength(3).WithMessage("Debes incluir minimo 3 caracteres")
                .MustAsync(TituloNoExiste);

            When(e => !string.IsNullOrEmpty(e.Detalle),
                () =>
                {
                    RuleFor(x => x.Detalle)
                        .MinimumLength(10).WithMessage("Debes incluir minimo 5 caracteres");
                });
        }

        private Task<bool> TituloNoExiste(string titulo, CancellationToken token)
        {
            return _repositorio.TituloNoExiste(titulo, token);
        }
    }

    public class CrearPreguntaManejador : IRequestHandler<CrearPreguntaComando, Respuesta<Exito>>
    {
        private readonly ICrearPreguntaRepositorio _repositorio;
        private readonly IValidator<CrearPreguntaComando> _validador;

        public CrearPreguntaManejador(ICrearPreguntaRepositorio repositorio, IValidator<CrearPreguntaComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Respuesta<Exito>> Handle(CrearPreguntaComando request, CancellationToken cancellationToken)
        {
            var validacion = await _validador.ValidateAsync(request, cancellationToken);

            if (!validacion.IsValid)
            {
                return new ErrorDeNegocio(
                    TipoDeError.ErrorDeValidation,
                    "Hay errores de validación en el formulario", 
                    Array.Empty<MensajeDeValidacion>());
            }

            var pregunta = new Pregunta(Guid.NewGuid(), request.Titulo, request.Detalle);

            return await _repositorio.Crear(pregunta, cancellationToken);
        }
    }

    public interface ICrearPreguntaRepositorio
    {
        Task<Respuesta<Exito>> Crear(Pregunta pregunta, CancellationToken cancellationToken);
        Task<bool> TituloNoExiste(string titulo, CancellationToken token);
    }

    public class CrearPreguntaRepositorio : ICrearPreguntaRepositorio
    {
        private readonly AppDbContext _context;

        public CrearPreguntaRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Respuesta<Exito>> Crear(Pregunta pregunta, CancellationToken cancellationToken)
        {
            _context.Preguntas.Add(pregunta);

            await _context.SaveChangesAsync(cancellationToken);

            return Exito.Valor;
        }

        public Task<bool> TituloNoExiste(string titulo, CancellationToken token)
        {
            return _context.Preguntas
                .Where(x => x.Titulo != titulo)
                .AnyAsync(token);
        }
    }
}
