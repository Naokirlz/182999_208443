using AutoMapper;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Incidentes.LogicaInterfaz;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private const string error_de_servidor = "Internal Server Error";
        private readonly IMapper _mapper;
        private readonly ILogicaProyecto _logica;

        public ProyectosController(ILogicaProyecto logica, IMapper mapper)
        {
            _logica = logica;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Proyecto proyecto)
        {
            try
            {
               if (ModelState.IsValid)
                {
                  Proyecto a = new Proyecto()
                  {
                        Id = proyecto.Id,
                        Nombre = proyecto.Nombre,
                  };

                    _logica.Alta("", a);
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

        [HttpDelete]
        public IActionResult Delete([FromBody] Proyecto proyecto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logica.Baja(proyecto.Id);
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
            return StatusCode(204, "resource deleted successfully");
        }
        
        [HttpPut]
        public IActionResult Put([FromBody] Proyecto proyecto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Proyecto a = new Proyecto()
                    {
                        Id = proyecto.Id,
                        Nombre = proyecto.Nombre,
                    };
                 _logica.Modificar(proyecto.Id, a);
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
            return StatusCode(202);
        }
    }
}