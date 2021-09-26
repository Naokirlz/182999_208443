using Incidentes.DatosInterfaz;
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
        private IRepositorioUsuario _repositorioUsuario;

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

        public void Save()
        {
            _contexto.SaveChanges();
        }
    }
}
