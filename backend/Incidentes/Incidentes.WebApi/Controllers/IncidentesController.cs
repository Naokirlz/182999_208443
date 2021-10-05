using AutoMapper;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentesController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly IMapper _mapper;
        private readonly ILogicaIncidente _logicaI;

        public IncidentesController(ILogicaIncidente logica, IMapper mapper)
        {
            _logicaI = logica;
            _mapper = mapper;
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
            if (incidente == null)
            {
                return NotFound(id);
            }

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