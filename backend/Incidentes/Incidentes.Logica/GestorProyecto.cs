using System;
using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;
using Incidentes.Logica.Interfaz;
using Incidentes.Logica.Excepciones;

namespace Incidentes.Logica
{
    public class GestorProyecto : ILogicaProyecto
    {
       IRepositorioGestores _repositorioGestor;

        public GestorProyecto(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }

        public void AgregarDesarrolladorAProyecto(int desarrollador, int idProyecto)
        {
            bool existeProyecto =_repositorioGestor.RepositorioProyecto.Existe(p => p.Id == idProyecto);
            if (existeProyecto)
            {
                Usuario aAgregar = _repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(d => d.Id == desarrollador, true).FirstOrDefault();

                if(aAgregar != null)
                {
                    Proyecto aModificar = _repositorioGestor.RepositorioProyecto.ObtenerPorCondicion(p => p.Id == idProyecto, true).FirstOrDefault();
                    aModificar.Desarrolladores.Add((Desarrollador)aAgregar);

                    _repositorioGestor.RepositorioProyecto.Modificar(aModificar);
                    _repositorioGestor.Save();

                }
                               
            }

        }

        public Proyecto Alta(Proyecto entity)
        {
            //Validamos que el objeto no sea null
            if (entity == null)
            {
                //O tambien si determino que el objeto es invalido mediante alguna otra regla....
                throw new Exception(); //Esto es solo una opcion, no necesariamente DEBE lanzarse una excepcion
            }
            _repositorioGestor.RepositorioProyecto.Alta(entity);
            _repositorioGestor.Save();

            return entity;
        }

        public void Baja(int id)
        {
            Proyecto aEliminar = Obtener(id);
            _repositorioGestor.RepositorioProyecto.Eliminar(aEliminar);
            _repositorioGestor.Save();

        }

        public Proyecto Modificar(int id, Proyecto entity)
        {
            Proyecto aModificar = Obtener(id);

            aModificar.Nombre = entity.Nombre;
            //federico: falta ver como modificar los demas atributos o en que lugar
            _repositorioGestor.RepositorioProyecto.Modificar(aModificar);
            _repositorioGestor.Save();
            return aModificar;
        }

        public Proyecto Obtener(int id)
        {
           Proyecto aObtener= _repositorioGestor.RepositorioProyecto.ObtenerPorCondicion(c => c.Id == id, true).FirstOrDefault();
            return aObtener;
        }

        public IEnumerable<Proyecto> ObtenerTodos()
        {
            return _repositorioGestor.RepositorioProyecto.ObtenerTodos(false);
        }
    }
}
