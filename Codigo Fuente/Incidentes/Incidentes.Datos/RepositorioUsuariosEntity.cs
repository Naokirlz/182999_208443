using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
                .AsNoTracking()
                .Count();
        }

        public List<Incidente> ListaDeIncidentesDeLosProyectosALosQuePertenece(int id, string proyecto, Incidente incidente)
        {
            Usuario usuario = this.ObtenerPorCondicion(d => d.Id == id, false).FirstOrDefault();
            IQueryable<Proyecto> proyectos = this.ListaDeProyectosALosQuePertenece(id);

            List<Incidente> listaIncidentes = new List<Incidente>();

            foreach (Proyecto p in proyectos)
            {
                if (proyecto == null || p.Nombre.ToLower().Contains(proyecto.ToLower()))
                {
                    foreach (Incidente i in p.Incidentes)
                    {
                        if ((incidente.Id == 0 || i.Id == incidente.Id)
                            && (incidente.Nombre == null || i.Nombre.ToLower().Contains(incidente.Nombre.ToLower()))
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

            if (usuario.RolUsuario != Usuario.Rol.Administrador)
            {
                return ContextoRepositorio.Set<Proyecto>()
                    .Include(p => p.Asignados)
                    .Include(p => p.Incidentes)
                    .Include(p => p.Tareas)
                    .Where(p => p.Asignados.Contains(usuario)).AsNoTracking();
            }
            return ContextoRepositorio.Set<Proyecto>()
                 .Include(p => p.Asignados)
                 .Include(p => p.Incidentes)
                 .Include(p => p.Tareas).AsNoTracking();
        }

        public List<Tarea> ListaDeTareasDeProyectosALosQuePertenece(int idUsuario)
        {
            Usuario usuario = this.ObtenerPorCondicion(d => d.Id == idUsuario, false).FirstOrDefault();
            IQueryable<Proyecto> proyectos = this.ListaDeProyectosALosQuePertenece(idUsuario);

            List<Tarea> listaTareas = new List<Tarea>();

            foreach (Proyecto p in proyectos)
            {
                foreach (Tarea t in p.Tareas)
                {
                    listaTareas.Add(t);
                }
            }

            return listaTareas;
        }
    }
}
