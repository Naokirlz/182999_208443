using System;

namespace Incidentes.Dominio
{
    public abstract class Usuario
    {

        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contrasenia { get; set; }
        public string Email { get; set; }


    }
}
