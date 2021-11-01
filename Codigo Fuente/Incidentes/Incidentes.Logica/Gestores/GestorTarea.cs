using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;
using Incidentes.Logica.Excepciones;

namespace Incidentes.Logica
{
    public class GestorTarea : ILogicaTarea
    {
        IRepositorioGestores _repositorioGestor;
        private const string acceso_no_autorizado = "Acceso no autorizado";
        private const string argumento_nulo = "El argumento no puede ser nulo";
        private const string usuario_no_pertenece = "El usuario no pertenece al proyecto";
        private const string elemento_no_existe = "El elemento no existe";
        private const string elemento_ya_existe = "Un elemento con similares atributos ya existe";
        private const int largo_maximo_nombre = 25;
        private const int largo_minimo_nombre = 5;

        public GestorTarea(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }

        public Tarea Alta(Tarea entity)
        {
            if (entity == null) throw new ExcepcionArgumentoNoValido(argumento_nulo);

            Validaciones.ValidarLargoTexto(entity.Nombre, largo_maximo_nombre, largo_minimo_nombre, "Nombre Tarea");
            Validaciones.ValidarMayorACero(entity.Costo, "Costo");
            Validaciones.ValidarMayorACero(entity.Duracion, "Duracion");

            _repositorioGestor.RepositorioTarea.Alta(entity);
            _repositorioGestor.Save();

            return entity;
        }

        public void Baja(int id)
        {
            Tarea aEliminar = Obtener(id);
            _repositorioGestor.RepositorioTarea.Eliminar(aEliminar);
            _repositorioGestor.Save();
        }

        public Tarea Modificar(int id, Tarea entity)
        {
            if (entity == null) throw new ExcepcionArgumentoNoValido(argumento_nulo);

            Tarea aModificar = Obtener(id);
            
            if (entity.Nombre != null)
            {
                Validaciones.ValidarLargoTexto(entity.Nombre, largo_maximo_nombre, largo_minimo_nombre, "Nombre Tarea");
                aModificar.Nombre = entity.Nombre;
            }
            if (entity.Costo != 0)
            {
                aModificar.Costo = entity.Costo;
                Validaciones.ValidarMayorACero(entity.Costo, "Costo");
            }
            if (entity.Duracion != 0)
            {
                Validaciones.ValidarMayorACero(entity.Duracion, "Duracion");
                aModificar.Duracion = entity.Duracion;
            }
            if (entity.ProyectoId != 0)
            {
                aModificar.ProyectoId = entity.ProyectoId;
            }

            _repositorioGestor.RepositorioTarea.Modificar(aModificar);
            _repositorioGestor.Save();
            return aModificar;
        }

        public Tarea Obtener(int id)
        {
            bool existe = _repositorioGestor.RepositorioTarea.Existe(c => c.Id == id);
            if (!existe) throw new ExcepcionElementoNoExiste(elemento_no_existe);
            Tarea aObtener = _repositorioGestor.RepositorioTarea.ObtenerPorCondicion(c => c.Id == id, true).FirstOrDefault();
            return aObtener;
        }

        public IEnumerable<Tarea> ObtenerTodos()
        {
            return _repositorioGestor.RepositorioTarea.ObtenerTodos(false);
        }
    }
}
