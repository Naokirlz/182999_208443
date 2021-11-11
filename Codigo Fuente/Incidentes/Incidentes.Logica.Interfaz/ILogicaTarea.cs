using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using System.Collections.Generic;

namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaTarea : ILogica<Tarea>
    {
        IEnumerable<Tarea> ListaDeTareasDeProyectosALosQuePertenece(int idUsu);
    }
}