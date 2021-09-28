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
        private const string argumento_nulo = "El argumento no puede ser nulo";
        private const string elemento_no_existe = "El elemento no existe";
        private const string elemento_ya_existe = "Un elemento con similares atributos ya existe";
        private const int largo_maximo_nombre = 25;
        private const int largo_minimo_nombre = 5;

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
            _repositorioGestor.RepositorioIncidente.Alta(entity);
            _repositorioGestor.Save();

            return entity;
        }

        public void Baja(int id)
        {
            Incidente aEliminar = Obtener(id);
            _repositorioGestor.RepositorioIncidente.Eliminar(aEliminar);
            _repositorioGestor.Save();
        }

        public Incidente Modificar(int id, Incidente entity)
        {
            throw new NotImplementedException();
        }

        public Incidente Obtener(int id)
        {
            bool existe = _repositorioGestor.RepositorioIncidente.Existe(c => c.Id == id);
            if (!existe) throw new ExcepcionElementoNoExiste(elemento_no_existe);
            Incidente aObtener = _repositorioGestor.RepositorioIncidente.ObtenerPorCondicion(c => c.Id == id, true).FirstOrDefault();
            return aObtener;
        }

        public IEnumerable<Incidente> ObtenerTodos()
        {
            return _repositorioGestor.RepositorioIncidente.ObtenerTodos(false);
        }

        private static void ValidarLargoTexto(string texto, int largoMax, int largoMin, string campo)
        {
            texto = texto.Trim();
            if (texto.Length > largoMax || texto.Length < largoMin)
                throw new ExcepcionLargoTexto("El largo del campo " + campo + " debe ser de entre " +
                                              largoMin.ToString() + " y " + largoMax.ToString() + " caracteres.");
        }
    }
}
