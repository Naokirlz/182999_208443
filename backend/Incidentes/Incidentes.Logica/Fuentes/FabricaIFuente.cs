using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;


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
