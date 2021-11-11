using Incidentes.Dominio;
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

        public TareasController(ILogicaTarea logicaT, ILogicaUsuario logicaU)
        {
            _logicaT = logicaT;
            _logicaU = logicaU;
        }

        [HttpGet]
        [FilterAutorizacion("Administrador", "Tester", "Desarrollador")]
        public IActionResult Get()
        {
            string token = Request.Headers["autorizacion"];
            Usuario usu = _logicaU.ObtenerPorToken(token);

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
        [FilterAutorizacion("Administrador")]
        public IActionResult Get(int id)
        {
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

        [HttpPut]
        [FilterAutorizacion("Administrador")]
        public IActionResult Put([FromBody] Tarea tarea)
        {
            _logicaT.Modificar(tarea.Id, tarea);
            return Ok(tarea);
        }
    }
}