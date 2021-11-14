using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Incidentes.WebApiTest
{
    public class UsuariosControllerTest
    {
        private Mock<ILogicaUsuario> _logicaU;
        private UsuariosController _dController;
        private List<Usuario> _usuariosL;

        [SetUp]
        public void Setup()
        {
            _logicaU = new Mock<ILogicaUsuario>();
            _dController = new UsuariosController(_logicaU.Object);
            _usuariosL = new List<Usuario>();
        }

        [TearDown]
        public void TearDown()
        {
            _logicaU = null;
            _dController = null;
            _usuariosL = null;
        }

        //[Test]
        //public void se_pueden_ver_los_desarrolladores()
        //{
        //    _usuariosL.Add(new Usuario() { 
        //        RolUsuario = Usuario.Rol.Desarrollador
        //    });
        //    _logicaU.Setup(c => c.Obtener(It.IsAny<Usuario.Rol>())).Returns(_usuariosL);

        //    var result = _dController.Get(Usuario.Rol.Desarrollador);

        //    Assert.IsNotNull(result);

        //    _logicaU.Verify(c => c.Obtener(It.IsAny<Usuario.Rol>()));
        //}

        //[Test]
        //public void se_pueden_ver_los_testers()
        //{
        //    _usuariosL.Add(new Usuario()
        //    {
        //        RolUsuario = Usuario.Rol.Tester
        //    });
        //    _logicaU.Setup(c => c.Obtener(It.IsAny<Usuario.Rol>())).Returns(_usuariosL);

        //    var result = _dController.Get(Usuario.Rol.Tester);

        //    Assert.IsNotNull(result);

        //    _logicaU.Verify(c => c.Obtener(It.IsAny<Usuario.Rol>()));
        //}

        [Test]
        public void se_puede_ver_un_usuario()
        {
            UsuarioDTO buscado = new UsuarioDTO()
            {
                RolUsuario = UsuarioDTO.Rol.Desarrollador
            };
            _logicaU.Setup(c => c.Obtener(3)).Returns(buscado);

            var result = _dController.Get(3);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(buscado, okResult.Value);

            _logicaU.Verify(c => c.Obtener(3));
        }

        [Test]
        public void se_puede_guardar_un_usuarior()
        {
            UsuarioDTO d = new UsuarioDTO()
            {
                RolUsuario = UsuarioDTO.Rol.Desarrollador
            };

            _logicaU.Setup(c => c.Alta(d)).Returns(d);

            var result = _dController.Post(d);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(d, okResult.Value);

            _logicaU.Verify(c => c.Alta(It.IsAny<UsuarioDTO>()));
        }
    }
}
