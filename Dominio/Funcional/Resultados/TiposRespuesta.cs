namespace CorteComun.Funcional.Resultados
{
    public enum TipoDeError
    {
        RecursoNoEncontrado,
        ErrorDeValidation
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
    }

    public readonly struct Exito
    {
        public static readonly Exito Valor = new Exito();
    }
}
