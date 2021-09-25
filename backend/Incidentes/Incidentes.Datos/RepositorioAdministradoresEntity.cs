using Incidentes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Datos
{
    public class RepositorioAdministradoresEntity
    {
        public RepositorioAdministradoresEntity()
        {
        }

        public void Alta(Administrador administrador)
        {
            using (Contexto context = new Contexto())
            {
                context.Administradores.Add(administrador);
                context.SaveChanges();
            }
        }

        public Administrador Obtener(int id)
        {
            using (Contexto context = new Contexto())
            {
                Administrador buscado;
                buscado = context.Administradores.Find(id);

                if (buscado != null)
                {
                    return buscado;
                }
                return null;
            }
        }
    }
}
