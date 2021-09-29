using AutoMapper;
using Incidentes.Dominio;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.WebApiTest
{
    public class DesarrolladoresControllerTest
    {
        private Mock<ILogicaUsuario> _logicaU;
        private Mock<IMapper> _mapper;
        private DesarrolladoresController _dController;
        private IQueryable<Usuario> desarrolladoresQ;
        private List<Desarrollador> desarrolladoresL;

        [SetUp]
        public void Setup()
        {
            _logicaU = new Mock<ILogicaUsuario>();
            _mapper = new Mock<IMapper>();
            _dController = new DesarrolladoresController(_logicaU.Object, _mapper.Object);
            desarrolladoresL = new List<Desarrollador>();
        }

        [TearDown]
        public void TearDown()
        {
            _logicaU = null;
            _mapper = null;
            _dController = null;
            desarrolladoresQ = null;
            desarrolladoresL = null;
        }

        [Test]
        public void se_pueden_ver_los_desarrolladores()
        {
            desarrolladoresL.Add(new Desarrollador());
            _logicaU.Setup(c => c.ObtenerDesarrolladores()).Returns(desarrolladoresL);

            var result = _dController.Get();

            Assert.IsNotNull(result);

            _logicaU.Verify(c => c.ObtenerDesarrolladores());
        }

        [Test]
        public void se_puede_ver_un_desarrollador()
        {
            Desarrollador buscado = new Desarrollador();
            _logicaU.Setup(c => c.ObtenerDesarrollador(3)).Returns(buscado);

            var result = _dController.Get(3);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(buscado, okResult.Value);

            _logicaU.Verify(c => c.ObtenerDesarrollador(3));
        }

        [Test]
        public void se_puede_guardar_un_desarrollador()
        {
            Desarrollador d = new Desarrollador()
            {
                Nombre = "Desarrollador"
            };

            _logicaU.Setup(c => c.Alta(d)).Returns(d);

            var result = _dController.Post(d);
            var okResult = result as OkObjectResult;

            Assert.AreEqual(d, okResult.Value);

            _logicaU.Verify(c => c.Alta(It.IsAny<Desarrollador>()));
        }
    }
}
