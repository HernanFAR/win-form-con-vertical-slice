using System;
using System.Threading.Tasks;
using Dominio.Funcional.Resultados;

namespace CorteComun.Funcional.Resultados
{
    public readonly struct Respuesta<TValor>
    {
        private readonly ErrorDeNegocio? _errorDeNegocio;
        private readonly TValor _valorExito;

        public bool Exito => _errorDeNegocio == null;

        public bool ConError => _errorDeNegocio != null;

        public TValor Valor => Equals(_valorExito, null)
            ? throw new InvalidOperationException(nameof(_valorExito))
            : _valorExito;

        public ErrorDeNegocio ErrorDeNegocio => _errorDeNegocio ?? throw new InvalidOperationException(nameof(_errorDeNegocio));

        public Respuesta(TValor valorExito)
        {
            _valorExito = valorExito;
            _errorDeNegocio = null;
        }

        public Respuesta(ErrorDeNegocio errorDeNegocio)
        {
            _valorExito = default;
            _errorDeNegocio = errorDeNegocio;
        }

        public static implicit operator Respuesta<TValor>(ErrorDeNegocio businessFailure) => new Respuesta<TValor>(businessFailure);
        public static implicit operator Respuesta<TValor>(TValor businessFailure) => new Respuesta<TValor>(businessFailure);
    }

    public static class RespuestasPorDefecto
    {
        public static readonly Respuesta<Exito> Exito = new Respuesta<Exito>(Dominio.Funcional.Resultados.Exito.Valor);

        public static Task<Respuesta<Exito>> Tarea => Task.FromResult(Exito);
    }
}
