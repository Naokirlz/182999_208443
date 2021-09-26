using System;
using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;

namespace Incidentes.Logica
{
    public class GestorUsuario : ILogicaUsuario
    {
        IRepositorioGestores _repositorioGestor;

        public GestorUsuario(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }


        public Usuario Modificar(int id, Usuario entity)
        {
            throw new NotImplementedException();
        }
        public bool Baja(int id)
        {
            throw new NotImplementedException();
        }

        public Usuario Obtener(int id)
        {
            var usuario = _repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(a => a.Id == id, trackChanges: false).FirstOrDefault();
            return usuario;
        }

        public IEnumerable<Usuario> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
        public Usuario Alta(Usuario usuario)
        {
            
            if (usuario == null)
            {
             throw new Exception(); 
            }
            _repositorioGestor.RepositorioUsuario.Crear(usuario);
            _repositorioGestor.Save();


            return usuario;
        }
    }
}
