using System;
using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;

namespace Incidentes.Logica
{
    public class GestorIncidente : ILogicaIncidente
    {
        IRepositorioGestores _repositorioGestor;

        public GestorIncidente(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }

        public Incidente Alta(Incidente entity)
        {
            if (entity == null)
            {               
                throw new Exception(); 
            }
            _repositorioGestor.RepositorioIncidente.Crear(entity);
            _repositorioGestor.Save();


            return entity;
        }

        public bool Baja(int id)
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

        public IEnumerable<Incidente> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
