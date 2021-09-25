using Incidentes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Datos
{
    public class RepositorioAdministradoresEntity : RepositorioBaseEntity<Administrador>, IRepositorioAdministrador
    {
        public RepositorioAdministradoresEntity(Contexto contextoRepositorio) : base(contextoRepositorio)
        {
        }

        public void Alta(Administrador entity) 
        {
            this.Crear(entity);
        }

        public Administrador Obtener(int id)
        {
            Administrador buscado;
            buscado = this.Obtener(id);

            if (buscado != null)
            {
                return buscado;
            }
            return null;
        }
    }
}
