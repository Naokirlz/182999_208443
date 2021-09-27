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
                .Include(p => p.Desarrolladores)
                .Include(p => p.Testers)
                .Include(p => p.Incidentes)
                .FirstOrDefault();
        }

        public IQueryable<Proyecto> ObtenerProyectosConIncidentes()
        {
            return ContextoRepositorio.Set<Proyecto>()
                .Include(p => p.Incidentes);
        }
    }
}