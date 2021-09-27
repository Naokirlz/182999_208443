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
    public class GestorMaestro<T> : ILogica<T>
    {
        IRepositorioGestores _repositorioGestor;
        private const string acceso_no_autorizado = "Acceso no autorizado";

        public GestorMaestro(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }

        public T Alta(string token, T entity)
        {
            throw new NotImplementedException();
        }

        public T Modificar(int id, T entity)
        {
            throw new NotImplementedException();
        }

        T ILogica<T>.Obtener(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<T> ILogica<T>.ObtenerTodos(string token)
        {
            throw new NotImplementedException();
        }

        public void Baja(int id)
        {
            throw new NotImplementedException();
        }
    }
}
