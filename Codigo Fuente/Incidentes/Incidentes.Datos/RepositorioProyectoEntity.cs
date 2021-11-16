using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Incidentes.Datos
{
    public class RepositorioProyectoEntity : RepositorioBase<Proyecto>, IRepositorioProyecto
    {
        public RepositorioProyectoEntity(Contexto contextoRepositorio) : base(contextoRepositorio)
        {
        }

        public override void Modificar(Proyecto proyecto)
        {
            using (ContextoRepositorio)
            {
                

                Proyecto aModificar = ContextoRepositorio.Set<Proyecto>()
                                        .Where(p => p.Id == proyecto.Id)
                                        .Include(p => p.Asignados)
                                        .Include(p => p.Incidentes)
                                        .Include(p => p.Tareas)
                                        .FirstOrDefault();

                aModificar.Incidentes = proyecto.Incidentes;
                aModificar.Tareas = proyecto.Tareas;
                aModificar.Nombre = proyecto.Nombre;

                aModificar.Asignados.Clear();
                ContextoRepositorio.SaveChanges();

                foreach (Usuario usu in proyecto.Asignados)
                {
                    Usuario nuevo = ContextoRepositorio.Set<Usuario>().Where(u => u.Id == usu.Id).FirstOrDefault();
                    aModificar.Asignados.Add(nuevo);
                    ContextoRepositorio.SaveChanges();
                }
                ContextoRepositorio.SaveChanges();
            }
        }

        public Proyecto ObtenerProyectoPorIdCompleto(int id)
        {
            return ContextoRepositorio.Set<Proyecto>()
                .Where(p => p.Id == id)
                .Include(p => p.Asignados)
                .Include(p => p.Incidentes)
                .Include(p => p.Tareas)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public IQueryable<Proyecto> ObtenerProyectosCompleto()
        {
            return ContextoRepositorio.Set<Proyecto>()
                .Include(p => p.Incidentes)
                .Include(p => p.Tareas)
                .Include(p => p.Asignados)
                .AsNoTracking();
        }

        public bool VerificarIncidentePerteneceAlProyecto(int idIncidente, int idProyecto)
        {
            Proyecto buscado = this.ObtenerProyectoPorIdCompleto(idProyecto);
            foreach (Incidente inc in buscado.Incidentes)
                if (inc.Id == idIncidente)
                    return true;
            return false;
        }

        public bool VerificarUsuarioPerteneceAlProyecto(int idUsuario, int idProyecto)
        {
            Proyecto buscado = this.ObtenerProyectoPorIdCompleto(idProyecto);
            foreach (Usuario usu in buscado.Asignados)
                if (usu.Id == idUsuario)
                    return true;

            return false;
        }
    }
}