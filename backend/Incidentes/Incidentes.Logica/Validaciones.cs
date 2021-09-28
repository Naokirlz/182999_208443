using Incidentes.Logica.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Logica
{
    internal class Validaciones
    {
        internal static void ValidarLargoTexto(string texto, int largoMax, int largoMin, string campo)
        {
            texto = texto.Trim();
            if (texto.Length > largoMax || texto.Length < largoMin)
                throw new ExcepcionLargoTexto("El largo del campo " + campo + " debe ser de entre " +
                                              largoMin.ToString() + " y " + largoMax.ToString() + " caracteres.");
        }
    }
}
