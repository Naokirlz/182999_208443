using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;

namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaUsuario : ILogica<Usuario>
    {
        public bool login(string nombreUSuario, string password);
    }
}
