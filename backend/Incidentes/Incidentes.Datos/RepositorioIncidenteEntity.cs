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
    public class RepositorioIncidenteEntity : RepositorioBase<Incidente>, IRepositorioIncidente
    {
        public RepositorioIncidenteEntity(Contexto contextoRepositorio) : base(contextoRepositorio)
        {
        }

        public void Alta(Incidente entity)
        {
            this.Crear(entity);
        }

        public Incidente Obtener(int id)
        {
            Incidente buscado;
            buscado = this.Obtener(id);

            if (buscado != null)
            {
                return buscado;
            }
            return null;
        }
    }
}
