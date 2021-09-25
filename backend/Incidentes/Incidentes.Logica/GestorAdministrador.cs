using Incidentes.Logica.Interfaz;
using System;
using System.Collections.Generic;
using Incidentes.Dominio;
using Incidentes.Datos;
using System.Linq;

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
        bool ILogica<Administrador>.Baja(int id)
        {
            throw new NotImplementedException();
        }

        Administrador ILogica<Administrador>.Obtener(int id)
        {
            var administrador = _repositorioGestor.RepositorioAdministradorEntity.ObtenerPorCondicion(a => a.Id == id, trackChanges: false).FirstOrDefault();
            return administrador;
        }

        IEnumerable<Administrador> ILogica<Administrador>.ObtenerTodos()
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
            _repositorioGestor.RepositorioAdministradorEntity.Crear(administrador);
            _repositorioGestor.Save();


            return administrador;
        }
    }
}
