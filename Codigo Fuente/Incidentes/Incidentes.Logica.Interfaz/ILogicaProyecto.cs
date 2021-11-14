using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.Logica.Interfaz;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaProyecto :ILogica<ProyectoDTO>
    {

        public void AgregarDesarrolladorAProyecto(List<int> idsUsuarios, int idProyecto);
        public bool VerificarUsuarioPerteneceAlProyecto(int idUsuario, int idProyecto);
        public IEnumerable<ProyectoDTO> ListaDeProyectosALosQuePertenece(int idUsuario);
        public ProyectoDTO ObtenerParaUsuario(int idUsuario, int idProyecto);
    }
}
