using Incidentes.DatosInterfaz;
using Incidentes.Logica.Excepciones;
using Incidentes.LogicaInterfaz;
using System;
using System.Linq;


namespace Incidentes.Logica
{
    public class GestorAutorizacion : ILogicaAutorizacion
    {

        IRepositorioGestores _repositorioGestor;
        private const string acceso_no_autorizado = "Acceso no autorizado";

        public GestorAutorizacion(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }

        public void UsuarioAutenticado(string token)
        {
            bool existeUsu = this._repositorioGestor.RepositorioUsuario.Existe(u => u.Token == token);
            if (!existeUsu)
                throw new ExcepcionAccesoNoAutorizado(acceso_no_autorizado);
        }

   
        public bool TokenValido(string authToken, params string[] roles)
        {
            if (string.IsNullOrEmpty(authToken))
            {
                return false;
            }
                        
            var usuario = _repositorioGestor.RepositorioUsuario
                .ObtenerPorCondicion(a => a.Token == authToken, trackChanges: false).FirstOrDefault();

            if (usuario == null)
            {
                return false;
            }

                          
            if (roles != null && roles.Length > 0)
            {
                if (!roles.Contains(usuario.RolUsuario.ToString()))
                {
                    return false;
                }
            }
            
            return true;

        }
    }
}
