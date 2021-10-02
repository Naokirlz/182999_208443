using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using System.Linq;

namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaProyecto :ILogica<Proyecto>
    {

        public void AgregarDesarrolladorAProyecto(int desarrollador, int idProyecto);
        public bool VerificarUsuarioPerteneceAlProyecto(int idUsuario, int idProyecto);
        public void ImportarBugs(string rutaFuente);
        public IQueryable<Proyecto> ListaDeProyectosALosQuePertenece(int idUsuario);
        public Proyecto ObtenerParaUsuario(int idUsuario, int idProyecto);
    }
}
