using Incidentes.DatosInterfaz;

namespace Incidentes.Datos
{
    public class RepositorioGestores : IRepositorioGestores
    {
        private Contexto _contexto;
        private IRepositorioUsuario _repositorioUsuario;
        private IRepositorioProyecto _repositorioProyecto;
        private IRepositorioIncidente _repositorioIncidente;
        private IRepositorioTarea _repositorioTarea;

        public RepositorioGestores(Contexto unContexto)
        {
            _contexto = unContexto;
        }

        public IRepositorioUsuario RepositorioUsuario
        {
            get
            {
                if (_repositorioUsuario == null)
                    _repositorioUsuario = new RepositorioUsuariosEntity(_contexto);

                return _repositorioUsuario;
            }
        }

        public IRepositorioProyecto RepositorioProyecto
        {
            get
            {
                if (_repositorioProyecto == null)
                    _repositorioProyecto = new RepositorioProyectoEntity(_contexto);

                return _repositorioProyecto;
            }
        }

        public IRepositorioIncidente RepositorioIncidente
        {
            get
            {
                if (_repositorioIncidente == null)
                    _repositorioIncidente = new RepositorioIncidenteEntity(_contexto);

                return _repositorioIncidente;
            }
        }
        public IRepositorioTarea RepositorioTarea
        {
            get
            {
                if (_repositorioTarea == null)
                    _repositorioTarea = new RepositorioTareaEntity(_contexto);

                return _repositorioTarea;
            }
        }

        public void Save()
        {
            _contexto.SaveChanges();
        }
    }
}
