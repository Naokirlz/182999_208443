using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Datos
{
    public class RepositorioGestores : IRepositorioGestores
    {
        private Contexto _contexto;
        private IRepositorioAdministrador _repositorioAdministrador;

        public RepositorioGestores(Contexto unContexto)
        {
            _contexto = unContexto;
        }

        public IRepositorioAdministrador RepositorioAdministradorEntity
        {
            get
            {
                if (_repositorioAdministrador == null)
                    _repositorioAdministrador = new RepositorioAdministradoresEntity(_contexto);

                return _repositorioAdministrador;
            }
        }

        public void Save()
        {
            _contexto.SaveChanges();
        }
    }
}
