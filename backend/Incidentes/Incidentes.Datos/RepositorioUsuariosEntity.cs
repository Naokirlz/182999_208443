using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable<Proyecto> ListaDeProyectosALosQuePertenece(int id)
        {
            //IQueryable<Proyecto> proyectos = ContextoRepositorio.Set<Proyecto>()
            //    .Include(p => p.Desarrolladores);
            Usuario usuario = this.ObtenerPorCondicion(d => d.Id == id, false).FirstOrDefault();

            if (usuario.GetType() == new Desarrollador().GetType())
            {
                return ContextoRepositorio.Set<Proyecto>()
                    .Include(p => p.Desarrolladores)
                    .Where(p => p.Desarrolladores.Contains((Desarrollador)usuario));
            }
            return ContextoRepositorio.Set<Proyecto>()
                    .Include(p => p.Testers)
                    .Where(p => p.Testers.Contains((Tester)usuario));
        }
    }
}
