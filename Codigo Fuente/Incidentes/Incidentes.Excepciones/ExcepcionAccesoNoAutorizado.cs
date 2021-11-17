using System;

namespace Incidentes.Excepciones
{
    public class ExcepcionAccesoNoAutorizado : Exception
    {
        public ExcepcionAccesoNoAutorizado() : base() { }

        public ExcepcionAccesoNoAutorizado(string message) : base(message) { }

        public ExcepcionAccesoNoAutorizado(string message, Exception innerException) : base(message, innerException) { }
    }
}
