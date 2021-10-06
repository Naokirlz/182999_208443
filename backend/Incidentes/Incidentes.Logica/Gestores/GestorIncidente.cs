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
            if (entity == null) throw new ExcepcionArgumentoNoValido(argumento_nulo);
            bool existe = _repositorioGestor.RepositorioIncidente.Existe(c => c.Nombre == entity.Nombre);
            if (existe) throw new ExcepcionArgumentoNoValido(elemento_ya_existe);

            Validaciones.ValidarLargoTexto(entity.Nombre, largo_maximo_nombre, largo_minimo_nombre, "Nombre Incidente");

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
            if (entity == null) throw new ExcepcionArgumentoNoValido(argumento_nulo);

            Incidente aModificar = Obtener(id);

            if (aModificar.Nombre != entity.Nombre)
            {
                bool existe = _repositorioGestor.RepositorioIncidente.Existe(c => c.Nombre == entity.Nombre);
                if (existe) throw new ExcepcionArgumentoNoValido(elemento_ya_existe);
                Validaciones.ValidarLargoTexto(entity.Nombre, largo_maximo_nombre, largo_minimo_nombre, "Nombre Incidente");
            }

            aModificar.Nombre = entity.Nombre;
            aModificar.EstadoIncidente = entity.EstadoIncidente;
            aModificar.ProyectoId = entity.ProyectoId;
            aModificar.Version = entity.Version;
            aModificar.Descripcion = entity.Descripcion;
            aModificar.DesarrolladorId = entity.DesarrolladorId;
           

            _repositorioGestor.RepositorioIncidente.Modificar(aModificar);
            _repositorioGestor.Save();
            return aModificar;
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

        public List<Incidente> ListaDeIncidentesDeLosProyectosALosQuePertenece(int idUsuario, string nombreProyecto, Incidente incidente)
        {
            return _repositorioGestor.RepositorioUsuario.ListaDeIncidentesDeLosProyectosALosQuePertenece(idUsuario, nombreProyecto, incidente);
        }

        public Incidente ObtenerParaUsuario(int idUsuario, int idIncidente)
        {
            Incidente inc = Obtener(idIncidente);
            if (!_repositorioGestor.RepositorioProyecto.VerificarIncidentePerteneceAlProyecto(idIncidente, inc.ProyectoId))
                throw new ExcepcionAccesoNoAutorizado(acceso_no_autorizado);
            if (!_repositorioGestor.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(idUsuario, inc.ProyectoId))
                throw new ExcepcionAccesoNoAutorizado(acceso_no_autorizado);
            return inc;
        }
    }
}
