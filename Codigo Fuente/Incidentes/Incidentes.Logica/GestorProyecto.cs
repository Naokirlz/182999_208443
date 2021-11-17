using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;
using Incidentes.DTOs;
using Incidentes.Excepciones;

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
            if (!existeProyecto)
                throw new ExcepcionElementoNoExiste(elemento_no_existe); 
            List<Usuario> asignados = new List<Usuario>();
            foreach(int idUsu in idsUsuarios)
            {
                Usuario aAgregar = _repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(d => d.Id == idUsu, false).FirstOrDefault();

                if (aAgregar != null)
                {
                    asignados.Add(aAgregar);
                }
            }
            Proyecto aModificar = _repositorioGestor.RepositorioProyecto.ObtenerProyectoPorIdCompleto(idProyecto);
            aModificar.Asignados = asignados;

            _repositorioGestor.RepositorioProyecto.Modificar(aModificar);
        }

        public ProyectoDTO Alta(ProyectoDTO entity)
        {
            if (entity == null) throw new ExcepcionArgumentoNoValido(argumento_nulo);
            bool existe = _repositorioGestor.RepositorioProyecto.Existe(c => c.Nombre == entity.Nombre);
            if (existe) throw new ExcepcionArgumentoNoValido(elemento_ya_existe);

            Validaciones.ValidarLargoTexto(entity.Nombre, largo_maximo_nombre, largo_minimo_nombre, "Nombre Proyecto");
            Proyecto alta = entity.convertirDTO_Dominio();
            _repositorioGestor.RepositorioProyecto.Alta(alta);
            _repositorioGestor.Save();

            return new ProyectoDTO(alta);
        }

        public void Baja(int id)
        {
            ProyectoDTO aEliminar = Obtener(id);

            _repositorioGestor.RepositorioProyecto.Eliminar(aEliminar.convertirDTO_Dominio());
            _repositorioGestor.Save();

        }

        public ProyectoDTO Modificar(int id, ProyectoDTO entity)
        {
            if (entity == null) throw new ExcepcionArgumentoNoValido(argumento_nulo);
            bool existe = _repositorioGestor.RepositorioProyecto.Existe(c => c.Id == id);
            if (!existe) throw new ExcepcionElementoNoExiste(elemento_no_existe);

            Proyecto aModificar = _repositorioGestor.RepositorioProyecto.ObtenerProyectoPorIdCompleto(id);

            if(aModificar.Nombre != entity.Nombre)
            {
                existe = _repositorioGestor.RepositorioProyecto.Existe(c => c.Nombre == entity.Nombre);
                if (existe) throw new ExcepcionArgumentoNoValido(elemento_ya_existe);
                Validaciones.ValidarLargoTexto(entity.Nombre, largo_maximo_nombre, largo_minimo_nombre, "Nombre Proyecto");
            }
            Proyecto mod = entity.convertirDTO_Dominio();
            _repositorioGestor.RepositorioProyecto.Modificar(mod);
            return new ProyectoDTO(mod);
        }

        public ProyectoDTO Obtener(int id)
        {
            bool existe = _repositorioGestor.RepositorioProyecto.Existe(c => c.Id == id);
            if (!existe) throw new ExcepcionElementoNoExiste(elemento_no_existe);
            Proyecto aObtener= _repositorioGestor.RepositorioProyecto.ObtenerProyectoPorIdCompleto(id);
            return new ProyectoDTO(aObtener);
        }

        public IEnumerable<ProyectoDTO> ObtenerTodos()
        {
            return convertirListaADTO(_repositorioGestor.RepositorioProyecto.ObtenerProyectosCompleto().ToList());
        }

        public bool VerificarUsuarioPerteneceAlProyecto(int idUsuario, int idProyecto)
        {
            return _repositorioGestor.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(idUsuario, idProyecto);
        }

        public IEnumerable<ProyectoDTO> ListaDeProyectosALosQuePertenece(int idUsuario)
        {
            return convertirListaADTO(_repositorioGestor.RepositorioUsuario.ListaDeProyectosALosQuePertenece(idUsuario).ToList());
        }

        public ProyectoDTO ObtenerParaUsuario(int idUsuario, int idProyecto)
        {
            if (!VerificarUsuarioPerteneceAlProyecto(idUsuario, idProyecto))
                throw new ExcepcionAccesoNoAutorizado(acceso_no_autorizado);
            IEnumerable<ProyectoDTO> proyectos = ObtenerTodos();
            ProyectoDTO proyecto = proyectos.Where(c => c.Id == idProyecto).FirstOrDefault();
            return proyecto;
        }

        private IEnumerable<ProyectoDTO> convertirListaADTO(List<Proyecto> proyectos)
        {
            List<ProyectoDTO> ret = new List<ProyectoDTO>();
            foreach (Proyecto t in proyectos)
            {
                ProyectoDTO nueva = new ProyectoDTO(t);
                ret.Add(nueva);
            }
            return ret;
        }
    }
}
