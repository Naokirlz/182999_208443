using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Excepciones
{
    public class ExcepcionArgumentoNoValido : Exception
    {
        public ExcepcionArgumentoNoValido() : base() { }

        public ExcepcionArgumentoNoValido(string message) : base(message) { }

        public ExcepcionArgumentoNoValido(string message, Exception innerException) : base(message, innerException) { }
    }
}
