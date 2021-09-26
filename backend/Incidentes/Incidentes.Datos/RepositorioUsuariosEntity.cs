using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Datos
{
    public class RepositorioUsuariosEntity : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuariosEntity(Contexto contextoRepositorio) : base(contextoRepositorio)
        {
        }

        public void Alta(Administrador entity) 
        {
            this.Crear(entity);
        }

        public Usuario Obtener(int id)
        {
            Usuario buscado;
            buscado = this.Obtener(id);

            if (buscado != null)
            {
                return buscado;
            }
            return null;
        }
    }
}
