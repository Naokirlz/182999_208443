using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;
using Incidentes.Logica.Excepciones;

namespace Incidentes.Logica
{
    public class GestorProyecto : ILogicaProyecto
    {
        IRepositorioGestores _repositorioGestor;
        private const string argumento_nulo = "El argumento no puede ser nulo";
        private const string elemento_no_existe = "El elemento no existe";
        private const string acceso_no_autorizado = "No tiene permisos para realizar dicha acción";
        private const string elemento_ya_existe = "Un elemento con similares atributos ya existe"; 
        private const int largo_maximo_nombre = 25;
        private const int largo_minimo_nombre = 5;

        public GestorProyecto(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }

        public void AgregarDesarrolladorAProyecto(List<int> idsUsuarios, int idProyecto)
        {
            bool existeProyecto =_repositorioGestor.RepositorioProyecto.Existe(p => p.Id == idProyecto);
            if (existeProyecto)
            {
                List<Usuario> asignados = new List<Usuario>();
                foreach(int idUsu in idsUsuarios)
                {
                    Usuario aAgregar = _repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(d => d.Id == idUsu, true).FirstOrDefault();

                    if (aAgregar != null)
                    {
                        asignados.Add(aAgregar);
                    }
                }
                Proyecto aModificar = _repositorioGestor.RepositorioProyecto.ObtenerPorCondicion(p => p.Id == idProyecto, true).FirstOrDefault();
                aModificar.Asignados = asignados;

                _repositorioGestor.RepositorioProyecto.Modificar(aModificar);
                _repositorioGestor.Save();
            }
        }

        public Proyecto Alta(Proyecto entity)
        {
            if (entity == null) throw new ExcepcionArgumentoNoValido(argumento_nulo);
            bool existe = _repositorioGestor.RepositorioProyecto.Existe(c => c.Nombre == entity.Nombre);
            if (existe) throw new ExcepcionArgumentoNoValido(elemento_ya_existe);

            Validaciones.ValidarLargoTexto(entity.Nombre, largo_maximo_nombre, largo_minimo_nombre, "Nombre Proyecto");

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
            if (entity == null) throw new ExcepcionArgumentoNoValido(argumento_nulo);

            Proyecto aModificar = Obtener(id);

            if(aModificar.Nombre != entity.Nombre)
            {
                bool existe = _repositorioGestor.RepositorioProyecto.Existe(c => c.Nombre == entity.Nombre);
                if (existe) throw new ExcepcionArgumentoNoValido(elemento_ya_existe);
                Validaciones.ValidarLargoTexto(entity.Nombre, largo_maximo_nombre, largo_minimo_nombre, "Nombre Proyecto");
            }

            aModificar.Nombre = entity.Nombre;
            aModificar.Asignados = entity.Asignados;
            aModificar.Incidentes = entity.Incidentes;

            _repositorioGestor.RepositorioProyecto.Modificar(aModificar);
            _repositorioGestor.Save();
            return aModificar;
        }

        public Proyecto Obtener(int id)
        {
            bool existe = _repositorioGestor.RepositorioProyecto.Existe(c => c.Id == id);
            if (!existe) throw new ExcepcionElementoNoExiste(elemento_no_existe);
            Proyecto aObtener= _repositorioGestor.RepositorioProyecto.ObtenerPorCondicion(c => c.Id == id, true).FirstOrDefault();
            return aObtener;
        }

        public IEnumerable<Proyecto> ObtenerTodos()
        {
            return _repositorioGestor.RepositorioProyecto.ObtenerProyectosCompleto();
        }

        public bool VerificarUsuarioPerteneceAlProyecto(int idUsuario, int idProyecto)
        {
            return _repositorioGestor.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(idUsuario, idProyecto);
        }

        public void ImportarBugs(string rutaFuente)
        {
            IFuente fuente = FabricaIFuente.FabricarIFuente(_repositorioGestor, rutaFuente);
            fuente.ImportarBugs();
        }

        public IQueryable<Proyecto> ListaDeProyectosALosQuePertenece(int idUsuario)
        {
            return _repositorioGestor.RepositorioUsuario.ListaDeProyectosALosQuePertenece(idUsuario);
        }

        public Proyecto ObtenerParaUsuario(int idUsuario, int idProyecto)
        {
            if (!VerificarUsuarioPerteneceAlProyecto(idUsuario, idProyecto))
                throw new ExcepcionAccesoNoAutorizado(acceso_no_autorizado);
            return Obtener(idProyecto);
        }
    }
}
