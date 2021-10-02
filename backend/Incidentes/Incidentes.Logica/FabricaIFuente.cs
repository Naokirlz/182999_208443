using Incidentes.DatosInterfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Logica
{
    internal class FabricaIFuente
    {
        internal static IFuente FabricarIFuente(IRepositorioGestores repositorioGestores, string rutaFuente)
        {
            if (rutaFuente.Contains(".xml"))
            {
                return new FuenteXML(repositorioGestores, rutaFuente);
            }
            return new FuenteTXT(repositorioGestores, rutaFuente);
        }
    }
}
