using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Excepciones
{
    public class ExcepcionNumeroNoValido : Exception
    {
        public ExcepcionNumeroNoValido() : base() { }

        public ExcepcionNumeroNoValido(string message) : base(message) { }

        public ExcepcionNumeroNoValido(string message, Exception innerException) : base(message, innerException) { }
    }
}
