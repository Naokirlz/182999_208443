using AutoMapper;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Incidentes.LogicaInterfaz;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogicaUsuario _logica;

        public LoginController(ILogicaUsuario logica)
        {
            _logica = logica;
        }

        [HttpPost]
        public IActionResult Login(Usuario usuario)
        {
            usuario.Token = _logica.Login(usuario.NombreUsuario, usuario.Contrasenia);
            return Ok(usuario.Token);
        }

        [HttpPost]
        public IActionResult Logout(Usuario usuario)
        {
            _logica.Logout(usuario.Token);
            return Ok();
        }
    }
}
