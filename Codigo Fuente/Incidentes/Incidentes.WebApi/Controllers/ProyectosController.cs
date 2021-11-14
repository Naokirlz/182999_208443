﻿using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.Logica.Excepciones;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Filters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [TrapExcepciones]
    [ApiController]
    public class ProyectosController : ControllerBase
    {
        private readonly ILogicaProyecto _logicaP;
        private readonly ILogicaUsuario _logicaU;
        private const string usuario_no_pertenece = "El usuario no pertenece al proyecto";
        private const string elemento_no_corresponde = "La entidad no corresponde con la enviada por parámetro";

        public ProyectosController(ILogicaProyecto logicaP, ILogicaUsuario logicaU)
        {
            _logicaP = logicaP;
            _logicaU = logicaU;
        }

        [HttpGet]
        [FilterAutorizacion("Administrador", "Tester", "Desarrollador")]
        public IActionResult Get()
        {
            string token = Request.Headers["autorizacion"];
            UsuarioDTO usu = _logicaU.ObtenerPorToken(token);

            List<Proyecto> result = new List<Proyecto>();

            if(usu.RolUsuario == 0)
            {
                result = _logicaP.ObtenerTodos().ToList();
            }
            else
            {
                result = _logicaP.ListaDeProyectosALosQuePertenece(usu.Id).ToList();
            }

            List<ProyectoDTO> proyectos = new List<ProyectoDTO>();
            foreach(Proyecto p in result)
            {
                ProyectoDTO pro = new ProyectoDTO()
                {
                    Id = p.Id,
                    Incidentes = p.Incidentes,
                    Tareas = p.Tareas,
                    Nombre = p.Nombre
                };
                foreach(Usuario u in p.Asignados)
                {
                    UsuarioDTO us = new UsuarioDTO(u);
                    pro.Asignados.Add(us);
                }
                proyectos.Add(pro);
            }
            return Ok(proyectos);
        }

        [HttpGet("{id}")]
        [FilterAutorizacion("Administrador", "Tester", "Desarrollador")]
        public IActionResult Get(int id)
        {
            string token = Request.Headers["autorizacion"];
            UsuarioDTO us = _logicaU.ObtenerPorToken(token);
            if(us.RolUsuario != UsuarioDTO.Rol.Administrador)
            {
                bool autorizado = _logicaP.VerificarUsuarioPerteneceAlProyecto(us.Id, id);
                if (!autorizado) throw new ExcepcionAccesoNoAutorizado(usuario_no_pertenece);
            }

            var p = _logicaP.Obtener(id);
            ProyectoDTO pro = new ProyectoDTO()
            {
                Id = p.Id,
                Incidentes = p.Incidentes,
                Tareas = p.Tareas,
                Nombre = p.Nombre
            };
            foreach (Usuario u in p.Asignados)
            {
                UsuarioDTO usu = new UsuarioDTO(u);
                pro.Asignados.Add(usu);
            }
            int duracion = 0;
            int costo = 0;
            foreach(Tarea t in p.Tareas)
            {
                duracion += t.Duracion;
                costo += t.Duracion * t.Costo;
            }
            foreach (Incidente i in p.Incidentes)
            {
                duracion += i.Duracion;
                if(i.EstadoIncidente == Incidente.Estado.Resuelto)
                {
                    Usuario u = p.Asignados.Find(d => d.Id == i.DesarrolladorId);
                    if (u != null)
                    {
                        costo += i.Duracion * u.ValorHora;
                    }
                }
            }
            pro.Duracion = duracion;
            pro.Costo = costo;
            return Ok(pro);
        }

        [HttpPost]
        [FilterAutorizacion("Administrador")]
        public IActionResult Post([FromBody] Proyecto proyecto)
        {
            _logicaP.Alta(proyecto);
            return Ok(proyecto);
        }

        [HttpDelete("{id}")]
        [FilterAutorizacion("Administrador")]
        public IActionResult Delete(int id)
        {
            _logicaP.Baja(id);
            return StatusCode(204);
        }
        
        [HttpPut("{id}")]
        [FilterAutorizacion("Administrador")]
        public IActionResult Put(int id, [FromBody] Proyecto proyecto)
        {
            if (id != proyecto.Id) throw new ExcepcionArgumentoNoValido(elemento_no_corresponde);
            _logicaP.Modificar(proyecto.Id, proyecto);
            ProyectoDTO pro = new ProyectoDTO()
            {
                Id = proyecto.Id,
                Incidentes = proyecto.Incidentes,
                Tareas = proyecto.Tareas,
                Nombre = proyecto.Nombre
            };
            foreach (Usuario u in proyecto.Asignados)
            {
                UsuarioDTO usu = new UsuarioDTO(u);
                pro.Asignados.Add(usu);
            }

            return Ok(pro);
        }
    }
}