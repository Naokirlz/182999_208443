using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;

namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaUsuario : ILogica<Usuario>
    {
        public bool Login(string nombreUSuario, string password);
        public void Logout(string tokenUsuario);
        public void AltaDesarrollador(string token, Usuario unDesarrollador);
    }
}
