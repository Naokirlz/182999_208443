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

        public TareasController(ILogicaTarea logica)
        {
            _logicaT = logica;
        }

        [HttpGet]
        [FilterAutorizacion("Administrador")]
        public IActionResult Get()
        {
            IEnumerable<Tarea> result = _logicaT.ObtenerTodos();
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