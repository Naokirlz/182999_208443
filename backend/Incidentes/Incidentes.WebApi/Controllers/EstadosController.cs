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
    public class EstadosController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly IMapper _mapper;
        private readonly ILogicaIncidente _logicaI;

        public EstadosController(ILogicaIncidente logica, IMapper mapper)
        {
            _logicaI = logica;
            _mapper = mapper;
        }

        [HttpPut]
        public IActionResult Put([FromBody] Incidente incidente)
        {
            try
            {
                Incidente aResolver = new Incidente()
                {
                    Id = incidente.Id,
                    EstadoIncidente = Incidente.Estado.Resuelto,
                    DesarrolladorId = incidente.DesarrolladorId
                };
                _logicaI.Modificar(incidente.Id, aResolver);
            }
            catch (ArgumentNullException nullex)
            {
                return UnprocessableEntity(nullex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, error_de_servidor);
            }
            return Ok(incidente);
        }
    }
}
