using Incidentes.DTOs;
using Incidentes.Excepciones;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [TrapExcepciones]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly ILogicaProyecto _logicaP;
        private readonly ILogicaUsuario _logicaU;
        private const string usuario_no_pertenece = "El usuario no pertenece al proyecto";
        private const string elemento_no_corresponde = "La entidad no corresponde con la enviada por parámetro";

        public ProyectosController(ILogicaProyecto logicaP, ILogicaUsuario logicaU)
        {
            _logicaP = logicaP;
            _logicaU = logicaU;
        }

        [HttpGet]
        [FilterAutorizacion("Administrador", "Tester", "Desarrollador")]
        public IActionResult Get()
        {
            string token = Request.Headers["autorizacion"];
            UsuarioDTO usu = _logicaU.ObtenerPorToken(token);

            List<ProyectoDTO> result = new List<ProyectoDTO>();

            if(usu.RolUsuario == 0)
            {
                result = _logicaP.ObtenerTodos().ToList();
            }
            else
            {
                result = _logicaP.ListaDeProyectosALosQuePertenece(usu.Id).ToList();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        [FilterAutorizacion("Administrador", "Tester", "Desarrollador")]
        public IActionResult Get(int id)
        {
            string token = Request.Headers["autorizacion"];
            UsuarioDTO us = _logicaU.ObtenerPorToken(token);
            if(us.RolUsuario != UsuarioDTO.Rol.Administrador)
            {
                bool autorizado = _logicaP.VerificarUsuarioPerteneceAlProyecto(us.Id, id);
                if (!autorizado) throw new ExcepcionAccesoNoAutorizado(usuario_no_pertenece);
            }

            var p = _logicaP.Obtener(id);
            return Ok(p);
        }

        [HttpPost]
        [FilterAutorizacion("Administrador")]
        public IActionResult Post([FromBody] ProyectoDTO proyecto)
        {
            _logicaP.Alta(proyecto);
            return Ok(proyecto);
        }

        [HttpDelete("{id}")]
        [FilterAutorizacion("Administrador")]
        public IActionResult Delete(int id)
        {
            _logicaP.Baja(id);
            return StatusCode(204);
        }
        
        [HttpPut("{id}")]
        [FilterAutorizacion("Administrador")]
        public IActionResult Put(int id, [FromBody] ProyectoDTO proyecto)
        {
            if (id != proyecto.Id) throw new ExcepcionArgumentoNoValido(elemento_no_corresponde);
            proyecto = _logicaP.Modificar(proyecto.Id, proyecto);

            return Ok(proyecto);
        }
    }
}