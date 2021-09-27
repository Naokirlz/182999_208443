using Incidentes.Dominio;
using System.Linq;

namespace Incidentes.DatosInterfaz
{
    public interface IRepositorioProyecto : IRepositorioBase<Proyecto>
    {
        public Proyecto ObtenerProyectoPorIdCompleto(int id);
    }
}
