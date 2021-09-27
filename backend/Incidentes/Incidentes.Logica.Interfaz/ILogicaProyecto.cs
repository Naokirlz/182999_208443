using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;


namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaProyecto :ILogica<Proyecto>
    {

        public void AgregarDesarrolladorAProyecto(int desarrollador, int idProyecto);

    }
}
