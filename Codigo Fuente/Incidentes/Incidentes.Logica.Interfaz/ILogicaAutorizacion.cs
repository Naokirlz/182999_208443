using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaAutorizacion
    {
        public void UsuarioAutenticado(string token);
        bool TokenValido(string authToken, params string[] roles);
    }
}
