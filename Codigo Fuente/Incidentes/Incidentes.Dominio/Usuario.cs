using System.Collections.Generic;

namespace Incidentes.Dominio
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contrasenia { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public Rol RolUsuario { get; set; }
        public virtual List<Proyecto> proyectos { get; set; }
        public double ValorHora { get; set; }
        public enum Rol
        {
            Administrador,
            Desarrollador,
            Tester
        }
    }
}
