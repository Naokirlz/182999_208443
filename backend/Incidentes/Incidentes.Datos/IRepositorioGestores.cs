using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Datos
{
    public interface IRepositorioGestores
    {
        IRepositorioAdministrador RepositorioAdministrador { get; }
        void Save();
    }
}
