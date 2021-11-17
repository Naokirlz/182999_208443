using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Excepciones
{
    public class ExcepcionElementoNoExiste : Exception
    {
        public ExcepcionElementoNoExiste() : base() { }

        public ExcepcionElementoNoExiste(string message) : base(message) { }

        public ExcepcionElementoNoExiste(string message, Exception innerException) : base(message, innerException) { }
    }
}
