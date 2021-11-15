using Incidentes.DTOs;
using System.Collections.Generic;

namespace Incidentes.LogicaInterfaz
{
    public interface IFuente
    {
        public List<ProyectoDTO> ImportarBugs(string rutaFuente);
    }
}
