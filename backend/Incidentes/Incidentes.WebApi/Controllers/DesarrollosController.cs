using AutoMapper;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesarrollosController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly IMapper _mapper;
        private readonly ILogicaProyecto _logica;

        public DesarrollosController(ILogicaProyecto logica, IMapper mapper)
        {
            _logica = logica;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AsignacionesDTO desarrollo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logica.AgregarDesarrolladorAProyecto(desarrollo.UsuarioId,desarrollo.ProyectoId);
                }
                else
                {
                    return UnprocessableEntity(ModelState);
                }
            }
            catch (ArgumentNullException nullex)
            {
                return UnprocessableEntity(nullex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, error_de_servidor);
            }
            return Ok();
        }


    }
}