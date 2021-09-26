using System;
using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;

namespace Incidentes.Logica
{
    public class GestorProyecto : ILogicaProyecto
    {
       IRepositorioGestores _repositorioGestor;

        public GestorProyecto(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }

        public Proyecto Alta(Proyecto entity)
        {
            //Validamos que el objeto no sea null
            if (entity == null)
            {
                //O tambien si determino que el objeto es invalido mediante alguna otra regla....
                throw new Exception(); //Esto es solo una opcion, no necesariamente DEBE lanzarse una excepcion
            }
            _repositorioGestor.RepositorioProyecto.Crear(entity);
            _repositorioGestor.Save();

            return entity;
        }

        public bool Baja(int id)
        {
            throw new NotImplementedException();
        }

        public Proyecto Modificar(int id, Proyecto entity)
        {
            throw new NotImplementedException();
        }

        public Proyecto Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proyecto> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
