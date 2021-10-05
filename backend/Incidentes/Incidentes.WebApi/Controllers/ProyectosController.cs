using AutoMapper;
using Incidentes.Dominio;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly IMapper _mapper;
        private readonly ILogicaProyecto _logicaP;

        public ProyectosController(ILogicaProyecto logica, IMapper mapper)
        {
            _logicaP = logica;
            _mapper = mapper;
        }

        [HttpGet]
        [FilterAutorizacion("Administrador")]
        public IActionResult Get()
        {
            IEnumerable<Proyecto> result = _logicaP.ObtenerTodos();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [FilterAutorizacion("Administrador")]
        public IActionResult Get(int id)
        {
            var proyecto = _logicaP.Obtener(id);
            if (proyecto == null)
            {
                return NotFound(id);
            }

            return Ok(proyecto);
        }

        [HttpPost]
        [FilterAutorizacion("Administrador")]
        public IActionResult Post([FromBody] Proyecto proyecto)
        {
            _logicaP.Alta(proyecto);
            return Ok(proyecto);
        }

        [HttpDelete]
        [FilterAutorizacion("Administrador")]
        public IActionResult Delete([FromBody] Proyecto proyecto)
        {
            _logicaP.Baja(proyecto.Id);
            return StatusCode(204, "Eliminado Satisfactoriamente.");
        }
        
        [HttpPut]
        [FilterAutorizacion("Administrador")]
        public IActionResult Put([FromBody] Proyecto proyecto)
        {
            _logicaP.Modificar(proyecto.Id, proyecto);
            return Ok(proyecto);
        }
    }
}