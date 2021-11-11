using Incidentes.Dominio;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.DatosInterfaz
{
    public interface IRepositorioUsuario : IRepositorioBase<Usuario>
    {
        public int CantidadDeIncidentesResueltosPorUnDesarrollador(int id);
        public IQueryable<Proyecto> ListaDeProyectosALosQuePertenece(int id);
        public List<Incidente> ListaDeIncidentesDeLosProyectosALosQuePertenece(int id, string proyecto, Incidente incidente);
        public List<Tarea> ListaDeTareasDeProyectosALosQuePertenece(int idUsuario);
    }
}
