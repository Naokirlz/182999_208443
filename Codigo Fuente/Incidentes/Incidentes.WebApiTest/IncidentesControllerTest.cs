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
    public class IncidentesControllerTest
    {
        private Mock<ILogicaIncidente> _logicaI;
        private Mock<ILogicaUsuario> _logicaU;
        private Mock<ILogicaProyecto> _logicaP;
        private IncidentesController _iController;
        private List<IncidenteDTO> incidentesL;

        [SetUp]
        public void Setup()
        {
            _logicaI = new Mock<ILogicaIncidente>();
            _logicaU = new Mock<ILogicaUsuario>();
            _logicaP = new Mock<ILogicaProyecto>();
            _iController = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            incidentesL = new List<IncidenteDTO>();
        }

        [TearDown]
        public void TearDown()
        {
            _logicaI = null;
            _logicaU = null;
            _logicaP = null;
            _iController = null;
            incidentesL = null;
        }

        [Test]
        public void se_pueden_ver_los_incidentes_a_los_que_pertenece()
        {
            IncidenteDTO i = new IncidenteDTO()
            {
                Id = 3,
                Nombre = "Incidente"
            };

            UsuarioDTO usu = new UsuarioDTO() { 
                Id = 3,
                RolUsuario = UsuarioDTO.Rol.Desarrollador
            };

            Proyecto pro = new Proyecto() { };
            incidentesL.Add(i);

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaI.Setup(c => c.ListaDeIncidentesDeLosProyectosALosQuePertenece(usu.Id, "", It.IsAny<IncidenteDTO>())).Returns(incidentesL);
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            var result = tested.Get();

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaI.Verify(c => c.ListaDeIncidentesDeLosProyectosALosQuePertenece(usu.Id, "", It.IsAny<IncidenteDTO>()));
        }

        [Test]
        public void administradorese_pueden_ver_los_incidentes()
        {
            IncidenteDTO i = new IncidenteDTO()
            {
                Id = 3,
                Nombre = "Incidente"
            };

            UsuarioDTO usu = new UsuarioDTO()
            {
                Id = 3,
                RolUsuario = UsuarioDTO.Rol.Administrador
            };

            Proyecto pro = new Proyecto() { };
            incidentesL.Add(i);

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaI.Setup(c => c.ObtenerTodos()).Returns(incidentesL);
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            var result = tested.Get();

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaI.Verify(c => c.ObtenerTodos());
        }

        [Test]
        public void se_pueden_ver_los_incidentes_de_los_proyectos_de_un_usuario()
        {
            incidentesL.Add(new IncidenteDTO());
            UsuarioDTO usu = new UsuarioDTO()
            {
                Id = 3,
                RolUsuario = UsuarioDTO.Rol.Desarrollador
            };

            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            _logicaI.Setup(c => c.ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IncidenteDTO>())).Returns(incidentesL);

            var result = tested.GetIncidentes("1", null, null);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(incidentesL, okResult.Value);

            _logicaI.Verify(c => c.ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IncidenteDTO>()));
            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
        }

        [Test]
        public void se_pueden_ver_los_incidentes_activos_de_los_proyectos_de_un_usuario()
        {
            incidentesL.Add(new IncidenteDTO());
            UsuarioDTO usu = new UsuarioDTO()
            {
                Id = 3,
                RolUsuario = UsuarioDTO.Rol.Desarrollador
            };

            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            _logicaI.Setup(c => c.ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IncidenteDTO>())).Returns(incidentesL);

            var result = tested.GetIncidentes("1", null, null,"Activo");
            var okResult = result as OkObjectResult;

            Assert.AreEqual(incidentesL, okResult.Value);

            _logicaI.Verify(c => c.ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IncidenteDTO>()));
            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
        }

        [Test]
        public void se_pueden_ver_los_incidentes_resueltos_de_los_proyectos_de_un_usuario()
        {
            incidentesL.Add(new IncidenteDTO());
            UsuarioDTO usu = new UsuarioDTO()
            {
                Id = 3,
                RolUsuario = UsuarioDTO.Rol.Desarrollador
            };

            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            _logicaI.Setup(c => c.ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IncidenteDTO>())).Returns(incidentesL);

            var result = tested.GetIncidentes("1", "ssss", null,"Resuelto");
            var okResult = result as OkObjectResult;

            Assert.AreEqual(incidentesL, okResult.Value);

            _logicaI.Verify(c => c.ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<IncidenteDTO>()));
            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
        }

        [Test]
        public void se_puede_ver_un_incidente()
        {
            IncidenteDTO i = new IncidenteDTO()
            {
                Id = 3,
                Nombre = "Incidente"
            };

            UsuarioDTO usu = new UsuarioDTO()
            {
                RolUsuario = UsuarioDTO.Rol.Tester
            };
            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaI.Setup(c => c.Obtener(It.IsAny<int>())).Returns(i);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            var result = tested.Get(1);

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaI.Verify(c => c.Obtener(It.IsAny<int>()));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void no_se_puede_ver_un_incidente_si_usuario_no_pertenece_proyecto()
        {
            IncidenteDTO i = new IncidenteDTO()
            {
                Id = 3,
                Nombre = "Incidente"
            };
            UsuarioDTO usu = new UsuarioDTO()
            {
                RolUsuario = UsuarioDTO.Rol.Desarrollador
            };
            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaI.Setup(c => c.Obtener(It.IsAny<int>())).Returns(i);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            Assert.Throws<ExcepcionAccesoNoAutorizado>(() => tested.Get(i.Id));

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaI.Verify(c => c.Obtener(It.IsAny<int>()));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void se_puede_guardar_un_incidente()
        {
            IncidenteDTO i = new IncidenteDTO()
            {
                Nombre = "Incidente",
                ProyectoId = 3
            };

            UsuarioDTO usu = new UsuarioDTO()
            {
                Id = 9,
                RolUsuario = UsuarioDTO.Rol.Tester
            };
            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _logicaI.Setup(c => c.Alta(i)).Returns(i);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            var result = tested.Post(i);

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaI.Verify(c => c.Alta(i));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void se_puede_guardar_un_incidente_resuleto()
        {
            IncidenteDTO i = new IncidenteDTO()
            {
                Nombre = "Incidente",
                EstadoIncidente = IncidenteDTO.Estado.Resuelto,
                ProyectoId = 3
            };

            UsuarioDTO usu = new UsuarioDTO()
            {
                Id = 9,
                RolUsuario = UsuarioDTO.Rol.Tester
            };
            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _logicaI.Setup(c => c.Alta(i)).Returns(i);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            var result = tested.Post(i);

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaI.Verify(c => c.Alta(i));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void no_se_puede_guardar_un_incidente_si_usuario_no_pertenece_proyecto()
        {
            IncidenteDTO i = new IncidenteDTO()
            {
                Nombre = "Incidente",
                ProyectoId = 3
            };

            UsuarioDTO usu = new UsuarioDTO()
            {
                RolUsuario = UsuarioDTO.Rol.Desarrollador
            };
            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            Assert.Throws<ExcepcionAccesoNoAutorizado>(() => tested.Post(i));

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void se_puede_actualizar_un_incidente()
        {
            IncidenteDTO i = new IncidenteDTO()
            {
                Id = 3,
                ProyectoId = 15,
                Nombre = "Incidente"
            };

            UsuarioDTO usu = new UsuarioDTO()
            {
                Id=9,
                RolUsuario = UsuarioDTO.Rol.Tester
            };
            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaI.Setup(c => c.Obtener(It.IsAny<int>())).Returns(i);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _logicaI.Setup(c => c.Modificar(3, i)).Returns(i);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            var result = tested.Put(3,i);

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaI.Verify(c => c.Obtener(It.IsAny<int>()));
            _logicaI.Verify(c => c.Modificar(3, i));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void se_puede_actualizar_un_incidente_resuelto()
        {
            IncidenteDTO i = new IncidenteDTO()
            {
                Id = 3,
                EstadoIncidente = IncidenteDTO.Estado.Resuelto,
                ProyectoId = 15,
                Nombre = "Incidente"
            };

            UsuarioDTO usu = new UsuarioDTO()
            {
                Id = 9,
                RolUsuario = UsuarioDTO.Rol.Tester
            };
            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaI.Setup(c => c.Obtener(It.IsAny<int>())).Returns(i);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _logicaI.Setup(c => c.Modificar(3, i)).Returns(i);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            var result = tested.Put(3, i);

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaI.Verify(c => c.Obtener(It.IsAny<int>()));
            _logicaI.Verify(c => c.Modificar(3, i));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void no_se_puede_actualizar_un_incidente_si_usuario_no_pertenece_proyecto()
        {
            IncidenteDTO i = new IncidenteDTO()
            {
                Id = 3,
                Nombre = "Incidente"
            };

            UsuarioDTO usu = new UsuarioDTO()
            {
                RolUsuario = UsuarioDTO.Rol.Tester
            };
            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaI.Setup(c => c.Obtener(It.IsAny<int>())).Returns(i);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            Assert.Throws<ExcepcionAccesoNoAutorizado>(() => tested.Put(3, i));

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaI.Verify(c => c.Obtener(It.IsAny<int>()));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void no_se_puede_actualizar_un_incidente_con_parametros_incorrectos()
        {
            IncidenteDTO i = new IncidenteDTO()
            {
                Id = 3,
                Nombre = "Incidente"
            };
            Assert.Throws<ExcepcionArgumentoNoValido>(() => _iController.Put(6, i));
        }

        [Test]
        public void se_puede_eliminar_un_incidente()
        {
            IncidenteDTO i = new IncidenteDTO()
            {
                Id = 3,
                Nombre = "Incidente"
            };

            UsuarioDTO usu = new UsuarioDTO()
            {
                RolUsuario = UsuarioDTO.Rol.Tester
            };
            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaI.Setup(c => c.Obtener(It.IsAny<int>())).Returns(i);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _logicaI.Setup(c => c.Baja(3));

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            var result = tested.Delete(i.Id);

            Assert.IsNotNull(result);

            _logicaI.Verify(c => c.Baja(3));
            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaI.Verify(c => c.Obtener(It.IsAny<int>()));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void no_se_puede_eliminar_un_incidente_si_usuario_no_pertenece_proyecto()
        {
            IncidenteDTO i = new IncidenteDTO()
            {
                Id = 3,
                Nombre = "Incidente"
            };
            UsuarioDTO usu = new UsuarioDTO() { 
                RolUsuario= UsuarioDTO.Rol.Desarrollador
            };
            Proyecto pro = new Proyecto() { };

            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(usu);
            _logicaI.Setup(c => c.Obtener(It.IsAny<int>())).Returns(i);
            _logicaP.Setup(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new IncidentesController(_logicaI.Object, _logicaU.Object, _logicaP.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            Assert.Throws<ExcepcionAccesoNoAutorizado>(() => tested.Delete(i.Id));

            _logicaU.Verify(c => c.ObtenerPorToken(It.IsAny<string>()));
            _logicaI.Verify(c => c.Obtener(It.IsAny<int>()));
            _logicaP.Verify(c => c.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }
    }
}