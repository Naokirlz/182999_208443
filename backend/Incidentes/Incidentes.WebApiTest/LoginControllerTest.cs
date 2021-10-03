using AutoMapper;
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
    public class LoginControllerTest
    {
        private Mock<ILogicaUsuario> _logicaU;
        private Mock<IMapper> _mapper;
        private LoginController _lController;
        private List<Usuario> _usuariosL;

        [SetUp]
        public void Setup()
        {
            _logicaU = new Mock<ILogicaUsuario>();
            _mapper = new Mock<IMapper>();
            _lController = new LoginController(_logicaU.Object, _mapper.Object);
            _usuariosL = new List<Usuario>();
        }

        [TearDown]
        public void TearDown()
        {
            _logicaU = null;
            _mapper = null;
            _lController = null;
            _usuariosL = null;
        }

        [Test]
        public void se_pueden_loguear_los_usuarios()
        {
            Usuario user = new Usuario()
            {
                NombreUsuario = "Nombre",
                Contrasenia = "123456"
            };
            _logicaU.Setup(c => c.Login(It.IsAny<string>(), It.IsAny<string>())).Returns("token");

            var result = _lController.Login(user);

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.Login(It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
