using Incidentes.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Contrasenia { get; set; }
        public Rol RolUsuario { get; set; }
        public int IncidentesResueltos { get; set; }
        public int ValorHora { get; set; }
        public enum Rol
        {
            Administrador,
            Desarrollador,
            Tester
        }

        public UsuarioDTO() { }
        public UsuarioDTO(Usuario usu)
        {
            Nombre = usu.Nombre;
            Apellido = usu.Apellido;
            Email = usu.Email;
            NombreUsuario = usu.NombreUsuario;
            Id = usu.Id;
            ValorHora = usu.ValorHora;
            Contrasenia = usu.Contrasenia;
            Token = usu.Token;

            if(usu.RolUsuario == Usuario.Rol.Administrador)
            {
                RolUsuario = Rol.Administrador;
            }else if(usu.RolUsuario == Usuario.Rol.Desarrollador)
            {
                RolUsuario = Rol.Desarrollador;
            }else
            {
                RolUsuario = Rol.Tester;
            }
        }

        public Usuario convertirDTO_Dominio()
        {
            Usuario usuario = new Usuario()
            {
                Nombre = this.Nombre,
                Apellido = this.Apellido,
                Email = this.Email,
                NombreUsuario = this.NombreUsuario,
                Id = this.Id,
                ValorHora = this.ValorHora,
                Contrasenia = this.Contrasenia,
                Token = this.Token,
            };
            if (this.RolUsuario == Rol.Administrador)
            {
                usuario.RolUsuario = Usuario.Rol.Administrador;
            }
            else if (this.RolUsuario == Rol.Desarrollador)
            {
                usuario.RolUsuario = Usuario.Rol.Desarrollador;
            }
            else
            {
                usuario.RolUsuario = Usuario.Rol.Tester;
            }
            return usuario;
        }
    }
}
