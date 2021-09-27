using System;
using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;
using Incidentes.Logica.Excepciones;

namespace Incidentes.Logica
{
    public class GestorIncidente : ILogicaIncidente
    {
        IRepositorioGestores _repositorioGestor;
        private const string acceso_no_autorizado = "Acceso no autorizado";
        public GestorIncidente(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }

        public Incidente Alta(string token, Incidente entity)
        {
            if (entity == null)
            {               
                throw new Exception(); 
            }
            _repositorioGestor.RepositorioIncidente.Alta(entity);
            _repositorioGestor.Save();


            return entity;
        }

        public void Baja(int id)
        {
            throw new NotImplementedException();
        }

        public Incidente Modificar(int id, Incidente entity)
        {
            throw new NotImplementedException();
        }

        public Incidente Obtener(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Incidente> ObtenerTodos(string token)
        {
            throw new NotImplementedException();
        }
        
    }
}
