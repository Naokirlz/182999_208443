using System;

namespace Incidentes.Logica.Excepciones
{
    public class ExcepcionArgumentoNoValido : Exception
    {
        public ExcepcionArgumentoNoValido() : base() { }

        public ExcepcionArgumentoNoValido(string message) : base(message) { }

        public ExcepcionArgumentoNoValido(string message, Exception innerException) : base(message, innerException) { }
    }
}