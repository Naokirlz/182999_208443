using Incidentes.DatosInterfaz;
using Incidentes.Logica.Excepciones;
using Incidentes.LogicaInterfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Logica
{
    public class GestorAutenticacion : ILogicaAutenticacion
    {

        IRepositorioGestores _repositorioGestor;
        private const string acceso_no_autorizado = "Acceso no autorizado";

        public GestorAutenticacion(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }

        public void UsuarioAutenticado(string token)
        {
            bool existeUsu = this._repositorioGestor.RepositorioUsuario.Existe(u => u.Token == token);
            if (!existeUsu)
                throw new ExcepcionAccesoNoAutorizado(acceso_no_autorizado);
        }
    }
}
