﻿using AutoMapper;
using Incidentes.Dominio;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.DTOs;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly ILogicaProyecto _logicaP;

        public ProyectosController(ILogicaProyecto logica)
        {
            _logicaP = logica;
        }

        [HttpGet]
        [FilterAutorizacion("Administrador")]
        public IActionResult Get()
        {
            List<Proyecto> result = _logicaP.ObtenerTodos().ToList();
            List<ProyectosDTO> proyectos = new List<ProyectosDTO>();
            foreach(Proyecto p in result)
            {
                ProyectosDTO pro = new ProyectosDTO()
                {
                    Id = p.Id,
                    Incidentes = p.Incidentes,
                    Nombre = p.Nombre
                };
                foreach(Usuario u in p.Asignados)
                {
                    UsuarioParaReporteDTO usu = new UsuarioParaReporteDTO()
                    {
                        Apellido = u.Apellido,
                        Email = u.Email,
                        Id = u.Id,
                        Nombre = u.Nombre,
                        NombreUsuario = u.NombreUsuario,
                        RolUsuario = u.RolUsuario
                    };
                    pro.Asignados.Add(usu);
                }
                proyectos.Add(pro);
            }
            return Ok(proyectos);
        }

        [HttpGet("{id}")]
        [FilterAutorizacion("Administrador")]
        public IActionResult Get(int id)
        {
            var proyecto = _logicaP.Obtener(id);
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