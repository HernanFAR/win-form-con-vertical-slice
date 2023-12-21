using System;
using CorteComun.Funcional.Resultados;

namespace Dominio
{
    public class Pregunta
    {
        public Guid Id { get; private set; }
        public string Titulo { get; private set; }
        public string Detalle { get; private set; }
        public bool Respondida { get; private set; }

        public Pregunta(Guid id, string titulo, string detalle)
        {
            Id = id;
            Titulo = titulo;
            Detalle = detalle;
        }

        public Respuesta<Exito> Editar(string titulo, string detalle)
        {
            Titulo = titulo;
            Detalle = detalle;

            return RespuestasPorDefecto.Exito;
        }

        public Respuesta<Exito> Responder()
        {
            Respondida = true;

            return RespuestasPorDefecto.Exito;
        }
    }
}
