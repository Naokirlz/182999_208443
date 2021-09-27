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

        public List<Incidente> ListaDeIncidentesDeLosProyectosALosQuePertenece(int id, string proyecto, Incidente incidente)
        {
            Usuario usuario = this.ObtenerPorCondicion(d => d.Id == id, false).FirstOrDefault();
            IQueryable<Proyecto> proyectos = this.ListaDeProyectosALosQuePertenece(id);

            List<Incidente> listaIncidentes = new List<Incidente>();

            foreach(Proyecto p in proyectos)
            {
                if (p.Nombre.Contains(proyecto))
                {
                    foreach (Incidente i in p.Incidentes)
                    {
                        if((incidente.Id == 0 || i.Id == incidente.Id) 
                            && (incidente.Nombre == null || i.Nombre.Contains(incidente.Nombre))
                            && (i.EstadoIncidente == incidente.EstadoIncidente || incidente.EstadoIncidente == Incidente.Estado.Indiferente))
                        {
                            listaIncidentes.Add(i);
                        }
                    }
                }
            }

            return listaIncidentes;

        }

        public IQueryable<Proyecto> ListaDeProyectosALosQuePertenece(int id)
        {
            Usuario usuario = this.ObtenerPorCondicion(d => d.Id == id, false).FirstOrDefault();

            if (usuario.GetType() == new Desarrollador().GetType())
            {
                return ContextoRepositorio.Set<Proyecto>()
                    .Include(p => p.Desarrolladores)
                    .Include(p => p.Incidentes)
                    .Where(p => p.Desarrolladores.Contains((Desarrollador)usuario));
            }
            return ContextoRepositorio.Set<Proyecto>()
                    .Include(p => p.Testers)
                    .Include(p => p.Incidentes)
                    .Where(p => p.Testers.Contains((Tester)usuario));
        }
    }
}
