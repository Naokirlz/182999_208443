using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;


namespace Incidentes.Logica
{
    internal class FabricaIFuente
    {
        internal static IFuente FabricarIFuente(IRepositorioGestores repositorioGestores, string rutaFuente, int usuarioId)
        {
            if (rutaFuente.Contains(".xml"))
            {
                return new FuenteXML(repositorioGestores, rutaFuente, usuarioId);
            }
            return new FuenteTXT(repositorioGestores, rutaFuente, usuarioId);
        }
    }
}
