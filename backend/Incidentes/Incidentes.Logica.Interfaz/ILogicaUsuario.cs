using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaUsuario : ILogica<Usuario>
    {
        public bool Login(string nombreUSuario, string password);
        public void Logout(string tokenUsuario);
        public int CantidadDeIncidentesResueltosPorUnDesarrollador(int idDesarrollador);
        public List<Incidente> ListaDeIncidentesDeLosProyectosALosQuePertenece(int idUsuario, string nombreProyecto, Incidente incidente);
        public IQueryable<Proyecto> ListaDeProyectosALosQuePertenece(int idUsuario, int idDesarrollador);
        public List<Usuario> ObtenerDesarrolladores();
        public Usuario ObtenerDesarrollador(int idDesarrollador);
        public List<Usuario> ObtenerTesters();
        public Usuario ObtenerTester(int idTester);
        public Usuario ObtenerPorToken(string token);
    }
}
