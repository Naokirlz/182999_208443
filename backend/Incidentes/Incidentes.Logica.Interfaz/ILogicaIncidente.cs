using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using System.Collections.Generic;

namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaIncidente : ILogica<Incidente>
    {
        public List<Incidente> ListaDeIncidentesDeLosProyectosALosQuePertenece(int idUsuario, string nombreProyecto, Incidente incidente);
    }
}
