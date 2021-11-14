using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.Logica.Interfaz;
using System.Collections.Generic;

namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaIncidente : ILogica<IncidenteDTO>
    {
        public List<IncidenteDTO> ListaDeIncidentesDeLosProyectosALosQuePertenece(int idUsuario, string nombreProyecto, IncidenteDTO incidente);
        public IncidenteDTO ObtenerParaUsuario(int idUsuario, int idIncidente);
    }
}
