using Incidentes.Logica.Interfaz;
using System;
using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;

namespace Incidentes.Logica
{
    public class GestorAdministrador : ILogicaAdministrador
    {
        IRepositorioGestores _repositorioGestor;

        public GestorAdministrador(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }


        public Administrador Modificar(int id, Administrador entity)
        {
            throw new NotImplementedException();
        }
        public bool Baja(int id)
        {
            throw new NotImplementedException();
        }

        public Administrador Obtener(int id)
        {
            var administrador = _repositorioGestor.RepositorioAdministrador.ObtenerPorCondicion(a => a.Id == id, trackChanges: false).FirstOrDefault();
            return administrador;
        }

        public IEnumerable<Administrador> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
        public Administrador Alta(Administrador administrador)
        {
            //Validamos que el objeto no sea null
            if (administrador == null)
            {
                //O tambien si determino que el objeto es invalido mediante alguna otra regla....
                throw new Exception(); //Esto es solo una opcion, no necesariamente DEBE lanzarse una excepcion
            }
            _repositorioGestor.RepositorioAdministrador.Crear(administrador);
            _repositorioGestor.Save();


            return administrador;
        }
    }
}
