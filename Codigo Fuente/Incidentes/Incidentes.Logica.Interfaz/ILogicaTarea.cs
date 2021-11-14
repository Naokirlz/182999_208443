using Incidentes.DTOs;
using Incidentes.Logica.Interfaz;
using System.Collections.Generic;

namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaTarea : ILogica<TareaDTO>
    {
        IEnumerable<TareaDTO> ListaDeTareasDeProyectosALosQuePertenece(int idUsu);
    }
}