using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.Logica.Excepciones;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [TrapExcepciones]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly ILogicaTarea _logicaT;
        private readonly ILogicaUsuario _logicaU;
        private readonly ILogicaProyecto _logicaP;
        private const string usuario_no_pertenece = "El usuario no pertenece al proyecto de la tarea";
        private const string elemento_no_corresponde = "La entidad no corresponde con la enviada por parámetro";

        public TareasController(ILogicaTarea logicaT, ILogicaUsuario logicaU, ILogicaProyecto logicaP)
        {
            _logicaT = logicaT;
            _logicaU = logicaU;
            _logicaP = logicaP;
        }

        [HttpGet]
        [FilterAutorizacion("Administrador", "Tester", "Desarrollador")]
        public IActionResult Get()
        {
            string token = Request.Headers["autorizacion"];
            UsuarioDTO usu = _logicaU.ObtenerPorToken(token);

            IEnumerable<Tarea> result = new List<Tarea>();

            if (usu.RolUsuario == 0)
            {
                result = _logicaT.ObtenerTodos();
            }
            else
            {
                result = _logicaT.ListaDeTareasDeProyectosALosQuePertenece(usu.Id);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        [FilterAutorizacion("Administrador", "Tester", "Desarrollador")]
        public IActionResult Get(int id)
        {
            string token = Request.Headers["autorizacion"];
            UsuarioDTO usu = _logicaU.ObtenerPorToken(token);
            Tarea tar = _logicaT.Obtener(id);
            if (usu.RolUsuario != 0)
            {
                bool autorizado = _logicaP.VerificarUsuarioPerteneceAlProyecto(usu.Id, tar.ProyectoId);
                if (!autorizado) throw new ExcepcionAccesoNoAutorizado(usuario_no_pertenece);
            }

            var tarea = _logicaT.Obtener(id);
            return Ok(tarea);
        }

        [HttpPost]
        [FilterAutorizacion("Administrador")]
        public IActionResult Post([FromBody] Tarea tarea)
        {
            _logicaT.Alta(tarea);
            return Ok(tarea);
        }

        [HttpDelete("{id}")]
        [FilterAutorizacion("Administrador")]
        public IActionResult Delete(int id)
        {
            _logicaT.Baja(id);
            return StatusCode(204);
        }

        [HttpPut("{id}")]
        [FilterAutorizacion("Administrador")]
        public IActionResult Put(int id, [FromBody] Tarea tarea)
        {
            if (id != tarea.Id) throw new ExcepcionArgumentoNoValido(elemento_no_corresponde);
            _logicaT.Modificar(tarea.Id, tarea);
            return Ok(tarea);
        }
    }
}