using System;

namespace Incidentes.Logica.Excepciones
{
    public class ExcepcionLargoTexto : Exception
    {
        public ExcepcionLargoTexto() : base() { }

        public ExcepcionLargoTexto(string message) : base(message) { }

        public ExcepcionLargoTexto(string message, Exception innerException) : base(message, innerException) { }

    }
}