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
    public class RepositorioProyectoEntity : RepositorioBase<Proyecto>, IRepositorioProyecto
    {
        public RepositorioProyectoEntity(Contexto contextoRepositorio) : base(contextoRepositorio)
        {
        }

        public void Alta(Proyecto entity)
        {
            this.Crear(entity);
        }

        public Proyecto Obtener(int id)
        {
            Proyecto buscado;
            buscado = this.Obtener(id);

            if (buscado != null)
            {
                return buscado;
            }
            return null;
        }
    }
}