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
    }
}
