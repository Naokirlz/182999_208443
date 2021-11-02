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
    public class IncidentesController : ControllerBase
    {
        private readonly ILogicaIncidente _logicaI;

        public IncidentesController(ILogicaIncidente logica)
        {
            _logicaI = logica;
        }

        [HttpGet]
        [FilterAutorizacion("Administrador")]
        public IActionResult Get()
        {
            IEnumerable<Incidente> result = _logicaI.ObtenerTodos();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [FilterAutorizacion("Administrador")]
        public IActionResult Get(int id)
        {
            var incidente = _logicaI.Obtener(id);
            return Ok(incidente);
        }

        [HttpPost]
        [FilterAutorizacion("Administrador", "Tester")]
        public IActionResult Post([FromBody] Incidente incidente)
        {
            _logicaI.Alta(incidente);
            return Ok(incidente);
        }

        [HttpDelete]
        [FilterAutorizacion("Administrador", "Tester")]
        public IActionResult Delete([FromBody] Incidente incidente)
        {
            _logicaI.Baja(incidente.Id);
            return StatusCode(204, "Eliminado Satisfactoriamente.");
        }

        [HttpPut]
        [FilterAutorizacion("Administrador", "Tester")]
        public IActionResult Put([FromBody] Incidente incidente)
        {
            _logicaI.Modificar(incidente.Id, incidente);
            return Ok(incidente);
        }
    }
}