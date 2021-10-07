using Incidentes.DatosInterfaz;
using Incidentes.Dominio;

namespace Incidentes.Datos
{
    public class RepositorioIncidenteEntity : RepositorioBase<Incidente>, IRepositorioIncidente
    {
        public RepositorioIncidenteEntity(Contexto contextoRepositorio) : base(contextoRepositorio)
        {
        }
    }
}
