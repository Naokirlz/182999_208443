using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaProyecto :ILogica<Proyecto>
    {

        public void AgregarDesarrolladorAProyecto(List<int> idsUsuarios, int idProyecto);
        public bool VerificarUsuarioPerteneceAlProyecto(int idUsuario, int idProyecto);
        //public void ImportarBugs(string rutaFuente, int usuarioId);
        public IQueryable<Proyecto> ListaDeProyectosALosQuePertenece(int idUsuario);
        public Proyecto ObtenerParaUsuario(int idUsuario, int idProyecto);
        //public void AgregarTareaAProyecto(List<int> idsTareas, int idProyecto);
    }
}
