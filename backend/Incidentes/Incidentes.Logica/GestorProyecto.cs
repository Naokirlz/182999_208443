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
        private const string acceso_no_autorizado = "Acceso no autorizado";

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

        public Proyecto Alta(string token, Proyecto entity)
        {
            UsuarioAutenticado(token);
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

        public IEnumerable<Proyecto> ObtenerTodos(string token)
        {
            UsuarioAutenticado(token);

            Usuario solicitante = _repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(u => u.Token == token, false).FirstOrDefault();

            return _repositorioGestor.RepositorioUsuario.ListaDeProyectosALosQuePertenece(solicitante.Id);
        }

        public void UsuarioAutenticado(string token)
        {
            bool existeUsu = this._repositorioGestor.RepositorioUsuario.Existe(u => u.Token == token);
            if (!existeUsu)
                throw new ExcepcionAccesoNoAutorizado(acceso_no_autorizado);
        }
    }
}
