using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.Excepciones;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApiTest
{
    public class ProyectosControllerTest
    {
        private Mock<ILogicaProyecto> _logicaP;
        private Mock<ILogicaUsuario> _logicaU;
        private ProyectosController _pController;
        private List<Proyecto> proyectosL;
        private UsuarioDTO u;
        private Incidente i;
        private Proyecto p;

        [SetUp]
        public void Setup()
        {
            _logicaP = new Mock<ILogicaProyecto>();
            _logicaU = new Mock<ILogicaUsuario>();
            _pController = new ProyectosController(_logicaP.Object, _logicaU.Object);
            proyectosL = new List<Proyecto>();
            i = new Incidente()
            {
                Id = 3,
                Version = "2.0",
                DesarrolladorId = 9,
                Descripcion = "descripcion",
                EstadoIncidente = Incidente.Estado.Resuelto,
                Nombre = "Nombre",
                Duracion = 2,
                ProyectoId = 7
            };
            u = new UsuarioDTO()
            {
                Nombre = "sssss",
                Id = 9,
                ValorHora = 200,
                Apellido = "aaaaaaa",
                Email = "ssasssa@asdasda.com",
                RolUsuario = UsuarioDTO.Rol.Desarrollador
            };
            p = new Proyecto()
            {
                Nombre = "Proyecto",
                Id = 7,
                Incidentes = new List<Incidente>() { i },
                Asignados = new List<Usuario>() { }
            };
        }

        [TearDown]
        public void TearDown()
        {
            _logicaP = null;
            _logicaU = null;
            _pController = null;
            proyectosL = null;
            u = null;
            i = null;
            p = null;
        }

        [Test]
        public void se_pueden_ver_los_proyectos()
        {
            ProyectoDTO p = new ProyectoDTO();
            UsuarioDTO u = new UsuarioDTO() {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                RolUsuario = UsuarioDTO.Rol.Tester,
                Email = "martint1@gmail.com",
                NombreUsuario = "martincosat1",
                Token = ""
            };
            p.Asignados.Add(u);

            List<ProyectoDTO> lista = new List<ProyectoDTO>();
            lista.Add(p);
            IQueryable<ProyectoDTO> pros = lista.AsQueryable();

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new ProyectosController(_logicaP.Object, _logicaU.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(u);
            _logicaP.Setup(c => c.ListaDeProyectosALosQuePertenece(It.IsAny<int>())).Returns(pros);

            var result = tested.Get();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaP.Verify(c => c.ListaDeProyectosALosQuePertenece(It.IsAny<int>()));
        }

        [Test]
        public void se_pueden_ver_los_proyectos_si_es_administrador()
        {
            ProyectoDTO p = new ProyectoDTO();
            UsuarioDTO u = new UsuarioDTO()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                RolUsuario = UsuarioDTO.Rol.Administrador,
                Email = "martint1@gmail.com",
                NombreUsuario = "martincosat1",
                Token = ""
            };
            p.Asignados.Add(u);
            List<ProyectoDTO> lista = new List<ProyectoDTO>();
            lista.Add(p);
            IQueryable<ProyectoDTO> pros = lista.AsQueryable();

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new ProyectosController(_logicaP.Object, _logicaU.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(u);
            _logicaP.Setup(c => c.ObtenerTodos()).Returns(pros);

            var result = tested.Get();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaP.Verify(c => c.ObtenerTodos());
        }

        [Test]
        public void se_puede_ver_un_proyecto()
        {
            UsuarioDTO us = new UsuarioDTO()
            {
                Id = 5,
                RolUsuario = UsuarioDTO.Rol.Administrador
            };
            ProyectoDTO p = new ProyectoDTO()
            {
                Nombre = "proyecto"
            };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(us);
            _logicaP.Setup(c => c.Obtener(1)).Returns(p);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new ProyectosController(_logicaP.Object, _logicaU.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            var result = tested.Get(1);

            Assert.IsNotNull(result);

            _logicaP.Verify(c => c.Obtener(1));
            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
        }

        [Test]
        public void se_puede_guardar_un_proyecto()
        {
            ProyectoDTO p = new ProyectoDTO()
            {
                Nombre = "proyecto"
            };

            _logicaP.Setup(c => c.Alta(p)).Returns(p);

            var result = _pController.Post(p);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(p, okResult.Value);

            _logicaP.Verify(c => c.Alta(It.IsAny<ProyectoDTO>()));
        }

        [Test]
        public void se_puede_actualizar_un_proyecto()
        {
            _logicaP.Setup(c => c.Modificar(It.IsAny<int>(), It.IsAny<ProyectoDTO>())).Returns(new ProyectoDTO(p));

            var result = _pController.Put(7, new ProyectoDTO(p));
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);

            _logicaP.Verify(c => c.Modificar(It.IsAny<int>(), It.IsAny<ProyectoDTO>()));
        }

        [Test]
        public void no_se_puede_actualizar_un_proyecto_con_parametros_incorrectos()
        {
            Assert.Throws<ExcepcionArgumentoNoValido>(() => _pController.Put(6, new ProyectoDTO(p)));
        }

        [Test]
        public void se_puede_eliminar_un_proyecto()
        {
            Proyecto p = new Proyecto()
            {
                Id = 3,
                Nombre = "proyecto"
            };

            _logicaP.Setup(c => c.Baja(3));

            var result = _pController.Delete(p.Id);

            Assert.IsNotNull(result);

            _logicaP.Verify(c => c.Baja(3));
        }

        [Test]
        public void se_puede_ver_el_costo_de_un_proyecto()
        {
            p.Asignados.Add(u.convertirDTO_Dominio());

            Tarea t = new Tarea()
            {
                Nombre = "Tarea",
                Duracion = 5,
                Costo = 1000,
                Id = 5,
                ProyectoId = p.Id
            };
            p.Tareas.Add(t);

            proyectosL.Add(p);
            IQueryable<Proyecto> pros = proyectosL.AsQueryable();

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(u);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new ProyectosController(_logicaP.Object, _logicaU.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";
            _logicaP.Setup(c => c.Obtener(7)).Returns(new ProyectoDTO(p));

            var result = tested.Get(7);
            var okResult = result as OkObjectResult;
            ProyectoDTO respuesta = (ProyectoDTO)okResult.Value;
            Assert.AreEqual(5400, respuesta.Costo);

            _logicaP.Verify(c => c.Obtener(7));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
        }

        [Test]
        public void se_puede_ver_la_duracion_de_un_proyecto()
        {
            p.Asignados.Add(u.convertirDTO_Dominio());
            Tarea t = new Tarea()
            {
                Nombre = "Tarea",
                Duracion = 5,
                Costo = 1000,
                Id = 5,
                ProyectoId = p.Id
            };
            p.Tareas.Add(t);

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(u);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new ProyectosController(_logicaP.Object, _logicaU.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            _logicaP.Setup(c => c.Obtener(7)).Returns(new ProyectoDTO(p));

            var result = tested.Get(7);
            var okResult = result as OkObjectResult;
            ProyectoDTO respuesta = (ProyectoDTO)okResult.Value;
            Assert.AreEqual(7, respuesta.Duracion);

            _logicaP.Verify(c => c.Obtener(7));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
        }
    }
}