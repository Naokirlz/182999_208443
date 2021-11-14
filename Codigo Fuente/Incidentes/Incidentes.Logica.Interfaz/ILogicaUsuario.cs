using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.Logica.Interfaz;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaUsuario : ILogica<UsuarioDTO>
    {
        public string Login(string nombreUSuario, string password);
        public void Logout(string tokenUsuario);
        public int CantidadDeIncidentesResueltosPorUnDesarrollador(int idDesarrollador);
        public UsuarioDTO ObtenerPorToken(string token);
    }
}
