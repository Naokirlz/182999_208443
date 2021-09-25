using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.DatosInterfaz
{
    public interface IRepositorioGestores
    {
        IRepositorioAdministrador RepositorioAdministrador { get; }
        void Save();
    }
}
