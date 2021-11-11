using Incidentes.Dominio;
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
            _tController = new TareasController(_logicaT.Object, _logicaU.Object);
            tareasL = new List<Tarea>();
            i = new Incidente()
            {
                Id = 3,
                Version = "2.0",
                UsuarioId = 1,
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
            Usuario u = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                RolUsuario = Usuario.Rol.Tester,
                Email = "martint1@gmail.com",
                NombreUsuario = "martincosat1",
                Token = ""
            };

            tareasL.Add(t);
            IQueryable<Tarea> tars = tareasL.AsQueryable();

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new TareasController(_logicaT.Object, _logicaU.Object);
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
            Usuario u = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                RolUsuario = Usuario.Rol.Administrador,
                Email = "martint1@gmail.com",
                NombreUsuario = "martincosat1",
                Token = ""
            };

            tareasL.Add(t);
            IQueryable<Tarea> tars = tareasL.AsQueryable();

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new TareasController(_logicaT.Object, _logicaU.Object);
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
            _logicaT.Setup(c => c.Obtener(1)).Returns(t);

            var result = _tController.Get(1);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(t, okResult.Value);

            _logicaT.Verify(c => c.Obtener(1));
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

            var result = _tController.Put(t);
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(result);

            _logicaT.Verify(c => c.Modificar(It.IsAny<int>(), It.IsAny<Tarea>()));
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