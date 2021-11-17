using Incidentes.DTOs;
using Incidentes.Excepciones;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Incidentes.WebApiTest
{
    public class LoginControllerTest
    {
        private Mock<ILogicaUsuario> _logicaU;
        private AutenticacionesController _lController;

        [SetUp]
        public void Setup()
        {
            _logicaU = new Mock<ILogicaUsuario>();
            _lController = new AutenticacionesController(_logicaU.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _logicaU = null;
            _lController = null;
        }

        [Test]
        public void se_pueden_loguear_los_usuarios()
        {
            UsuarioDTO user = new UsuarioDTO()
            {
                NombreUsuario = "Nombre",
                Contrasenia = "123456"
            };
            _logicaU.Setup(c => c.Login(It.IsAny<string>(), It.IsAny<string>())).Returns("token");

            var result = _lController.Login(user);

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.Login(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Test]
        public void se_pueden_desloguear_los_usuarios()
        {
            UsuarioDTO user = new UsuarioDTO()
            {
                Id = 7,
                Token = "asdadsacasc"
            };
            _logicaU.Setup(c => c.Logout(It.IsAny<string>()));
            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(user);
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new AutenticacionesController(_logicaU.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            var result = tested.Logout(user.Id);

            Assert.IsNotNull(result);

            _logicaU.VerifyAll();
        }

        [Test]
        public void no_se_pueden_desloguear_otros_usuarios()
        {
            UsuarioDTO user = new UsuarioDTO()
            {
                Id = 7,
                Token = "asdadsacasc"
            };
            _logicaU.Setup(c => c.ObtenerPorToken(It.IsAny<string>())).Returns(user);
            var ctx = new ControllerContext() { HttpContext = new DefaultHttpContext() };
            var tested = new AutenticacionesController(_logicaU.Object);
            tested.ControllerContext = ctx;
            ctx.HttpContext.Request.Headers["autorizacion"] = "aaa";

            Assert.Throws<ExcepcionAccesoNoAutorizado>(() => tested.Logout(12));

            _logicaU.VerifyAll();
        }
    }
}
