using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Datos
{
    public class RepositorioUsuariosEntity : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuariosEntity(Contexto contextoRepositorio) : base(contextoRepositorio)
        {
        }

        public int CantidadDeIncidentesResueltosPorUnDesarrollador(int id)
        {
            return ContextoRepositorio.Set<Incidente>()
                .Where(i => (i.DesarrolladorId == id) && (i.EstadoIncidente == Incidente.Estado.Resuelto))
                .Count();
        }
    }
}
