using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.Logica.Excepciones;
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
    public class TareasControllerTest
    {
        private Mock<ILogicaTarea> _logicaT;
        private Mock<ILogicaUsuario> _logicaU;
        private Mock<ILogicaProyecto> _logicaP;
        private TareasController _tController;
        private List<Tarea> tareasL;
        private Usuario u;
        private Incidente i;
        private Proyecto p;
        private Tarea t;

        [SetUp]
        public void Setup()
        {
            _logicaT = new Mock<ILogicaTarea>();
            _logicaU = new Mock<ILogicaUsuario>();
            _logicaP = new Mock<ILogicaProyecto>();
            _tController = new TareasController(_logicaT.Object, _logicaU.Object, _logicaP.Object);
            tareasL = new List<Tarea>();
            i = new Incidente()
            {
                Id = 3,
                Version = "2.0",
                DesarrolladorId = 5,
                Descripcion = "descripcion",
                EstadoIncidente = Incidente.Estado.Resuelto,
                Nombre = "Nombre",
                ProyectoId = 7
            };
            u = new Usuario()
            {
                Nombre = "sssss",
                Id = 9,
                Apellido = "aaaaaaa",
                Email = "ssasssa@asdasda.com",
                RolUsuario = Usuario.Rol.Desarrollador
            };
            p = new Proyecto()
            {
                Nombre = "Proyecto",
                Id = 7,
                Incidentes = new List<Incidente>() { i },
                Asignados = new List<Usuario>() { u }
            };
            t = new Tarea()
            {
                Nombre = "Tarea nueva",
                Costo = 500,
                Duracion = 1,
                ProyectoId = 7,
                Id = 8
            };
        }

        [TearDown]
        public void TearDown()
        {
            _logicaT = null;
            _logicaU = null;
            _logicaP = null;
            _tController = null;
            tareasL = null;
            u = null;
            i = null;
            p = null;
            t = null;
        }

        [Test]
        public void se_pueden_ver_las_tareas()
        {
            Tarea t = new Tarea();
            UsuarioDTO u = new UsuarioDTO()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                RolUsuario = UsuarioDTO.Rol.Tester,
                Email = "martint1@gmail.com",
                NombreUsuario = "martincosat1",
                Token = ""
            };

            tareasL.Add(t);
            IQueryable<Tarea> tars = tareasL.AsQueryable();

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new TareasController(_logicaT.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(u);
            _logicaT.Setup(c => c.ListaDeTareasDeProyectosALosQuePertenece(It.IsAny<int>())).Returns(tars);

            var result = tested.Get();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaT.Verify(c => c.ListaDeTareasDeProyectosALosQuePertenece(It.IsAny<int>()));
        }

        [Test]
        public void los_administradores_pueden_ver_las_tareas()
        {
            Tarea t = new Tarea();
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

            tareasL.Add(t);
            IQueryable<Tarea> tars = tareasL.AsQueryable();

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new TareasController(_logicaT.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(u);
            _logicaT.Setup(c => c.ObtenerTodos()).Returns(tars);

            var result = tested.Get();
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaT.Verify(c => c.ObtenerTodos());
        }

        [Test]
        public void se_puede_ver_una_tarea()
        {
            Tarea t = new Tarea()
            {
                Id = 3,
                Nombre = "Tarea"
            };

            UsuarioDTO usu = new UsuarioDTO()
            {
                RolUsuario = UsuarioDTO.Rol.Desarrollador
            };
            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaT.Setup(c => c.Obtener(It.IsAny<int>())).Returns(t);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new TareasController(_logicaT.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            var result = tested.Get(1);

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaT.Verify(c => c.Obtener(It.IsAny<int>()));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }
        [Test]
        public void no_se_puede_ver_una_tarea_si_usuario_no_pertenece_proyecto()
        {
            Tarea t = new Tarea()
            {
                Id = 3,
                Nombre = "Tarea"
            };

            UsuarioDTO usu = new UsuarioDTO() {
                RolUsuario = UsuarioDTO.Rol.Desarrollador
            };
            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaT.Setup(c => c.Obtener(It.IsAny<int>())).Returns(t);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new TareasController(_logicaT.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            Assert.Throws<ExcepcionAccesoNoAutorizado>(() => tested.Get(1));

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaT.Verify(c => c.Obtener(It.IsAny<int>()));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }


        [Test]
        public void se_puede_guardar_una_tarea()
        {
            _logicaT.Setup(c => c.Alta(t)).Returns(t);

            var result = _tController.Post(t);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(t, okResult.Value);

            _logicaT.Verify(c => c.Alta(It.IsAny<Tarea>()));
        }

        [Test]
        public void se_puede_actualizar_una_tarea()
        {
            _logicaT.Setup(c => c.Modificar(It.IsAny<int>(), It.IsAny<Tarea>())).Returns(t);

            var result = _tController.Put(8, t);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);

            _logicaT.Verify(c => c.Modificar(It.IsAny<int>(), It.IsAny<Tarea>()));
        }

        [Test]
        public void no_se_puede_actualizar_una_tarea_con_parametros_incorrectos()
        {
            Assert.Throws<ExcepcionArgumentoNoValido>(() => _tController.Put(6, t));
        }

        [Test]
        public void se_puede_eliminar_una_tarea()
        {
            _logicaT.Setup(c => c.Baja(8));

            var result = _tController.Delete(8);

            Assert.IsNotNull(result);

            _logicaT.Verify(c => c.Baja(8));
        }
    }
}