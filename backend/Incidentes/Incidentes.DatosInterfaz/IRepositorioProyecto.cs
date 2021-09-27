using Incidentes.Dominio;
using System.Linq;

namespace Incidentes.DatosInterfaz
{
    public interface IRepositorioProyecto : IRepositorioBase<Proyecto>
    {
        public Proyecto ObtenerProyectoPorIdCompleto(int id);
        public IQueryable<Proyecto> ObtenerProyectosConIncidentes();
        public bool VerificarUsuarioPerteneceAlProyecto(int idUsuario, int idProyecto);
        public bool VerificarIncidentePerteneceAlProyecto(int idIncidente, int idProyecto);
    }
}
