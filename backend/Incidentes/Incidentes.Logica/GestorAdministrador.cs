using Incidentes.Datos;
using Incidentes.Dominio;
using System;

namespace Incidentes.Logica
{
    public class GestorAdministrador
    {
        private RepositorioAdministradoresEntity repositorio = new RepositorioAdministradoresEntity();

        public void Alta(Administrador administrador)
        {

            repositorio.Alta(administrador);


        }

        public Administrador Obtener(int id)
        {
           return repositorio.Obtener(id);
        }


    }
}
