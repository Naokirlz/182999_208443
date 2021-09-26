using Incidentes.Logica.Interfaz;
using System;
using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;

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
            //Validamos que el objeto no sea null
            if (usuario == null)
            {
                //O tambien si determino que el objeto es invalido mediante alguna otra regla....
                throw new Exception(); //Esto es solo una opcion, no necesariamente DEBE lanzarse una excepcion
            }
            _repositorioGestor.RepositorioUsuario.Crear(usuario);
            _repositorioGestor.Save();


            return usuario;
        }
    }
}
