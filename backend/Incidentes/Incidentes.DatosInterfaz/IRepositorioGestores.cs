using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.DatosInterfaz
{
    public interface IRepositorioGestores
    {
        IRepositorioUsuario RepositorioUsuario { get; }
        IRepositorioProyecto RepositorioProyecto { get; }
        IRepositorioIncidente RepositorioIncidente{ get; }
        void Save();
    }
}
