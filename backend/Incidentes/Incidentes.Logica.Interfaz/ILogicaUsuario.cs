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
        public List<Usuario> Obtener(Usuario.Rol? rol = null);
        public Usuario ObtenerPorToken(string token);
    }
}
