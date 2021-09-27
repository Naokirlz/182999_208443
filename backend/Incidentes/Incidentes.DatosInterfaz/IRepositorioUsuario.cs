using Incidentes.Dominio;
using System.Linq;

namespace Incidentes.DatosInterfaz
{
    public interface IRepositorioUsuario : IRepositorioBase<Usuario>
    {
        public int CantidadDeIncidentesResueltosPorUnDesarrollador(int id);
        public IQueryable<Proyecto> ListaDeProyectosALosQuePertenece(int id);
    }
}
