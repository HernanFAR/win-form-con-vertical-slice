using System;

namespace Dominio.Funcional.Resultados
{
    public enum TipoDeError
    {
        RecursoNoEncontrado,
        ErrorDeValidation,
        Desconocido
    }

    public readonly struct MensajeDeValidacion
    {
        public string Propiedad { get; }
        public string Mensaje { get; }

        public MensajeDeValidacion(string propiedad, string mensaje)
        {
            Propiedad = propiedad;
            Mensaje = mensaje;
        }
    }

    public readonly struct ErrorDeNegocio
    {   
        public TipoDeError Tipo { get; }
        public string Mensaje { get; }
        public MensajeDeValidacion[] MensajesDeValidacion { get; }

        public ErrorDeNegocio(TipoDeError tipo, string mensaje, MensajeDeValidacion[] mensajesDeValidacion)
        {
            Tipo = tipo;
            Mensaje = mensaje;
            MensajesDeValidacion = mensajesDeValidacion;
        }

        public ErrorDeNegocio(TipoDeError tipo) 
            : this(tipo, string.Empty, Array.Empty<MensajeDeValidacion>()) { }
    }

    public readonly struct Exito
    {
        public static readonly Exito Valor = new Exito();
    }
}
