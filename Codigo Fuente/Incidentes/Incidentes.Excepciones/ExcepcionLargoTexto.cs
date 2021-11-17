using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Excepciones
{
    public class ExcepcionLargoTexto : Exception
    {
        public ExcepcionLargoTexto() : base() { }

        public ExcepcionLargoTexto(string message) : base(message) { }

        public ExcepcionLargoTexto(string message, Exception innerException) : base(message, innerException) { }
    }
}
