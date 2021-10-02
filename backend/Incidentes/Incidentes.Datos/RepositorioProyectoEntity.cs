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
    public class RepositorioProyectoEntity : RepositorioBase<Proyecto>, IRepositorioProyecto
    {
        public RepositorioProyectoEntity(Contexto contextoRepositorio) : base(contextoRepositorio)
        {
        }

        public Proyecto ObtenerProyectoPorIdCompleto(int id)
        {
            return ContextoRepositorio.Set<Proyecto>()
                .Where(p => p.Id == id)
                .Include(p => p.Asignados)
                .Include(p => p.Incidentes)
                .FirstOrDefault();
        }

        public IQueryable<Proyecto> ObtenerProyectosConIncidentes()
        {
            return ContextoRepositorio.Set<Proyecto>()
                .Include(p => p.Incidentes);
        }

        public bool VerificarIncidentePerteneceAlProyecto(int idIncidente, int idProyecto)
        {
            Proyecto buscado = this.ObtenerProyectoPorIdCompleto(idProyecto);
            Incidente incidente = ContextoRepositorio.Set<Incidente>().Where(i => i.Id == idIncidente).FirstOrDefault();

            return buscado.Incidentes.Contains(incidente);
        }

        public bool VerificarUsuarioPerteneceAlProyecto(int idUsuario, int idProyecto)
        {
            Proyecto buscado = this.ObtenerProyectoPorIdCompleto(idProyecto);
            Usuario solicitante = ContextoRepositorio.Set<Usuario>().Where(u => u.Id == idUsuario).FirstOrDefault();

            return buscado.Asignados.Contains(solicitante) || solicitante.RolUsuario == Usuario.Rol.Administrador;
        }
    }
}