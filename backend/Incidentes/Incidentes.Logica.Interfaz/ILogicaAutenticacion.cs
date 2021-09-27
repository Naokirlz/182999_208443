using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaAutenticacion
    {
        public void UsuarioAutenticado(string token);
    }
}
