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
    public class ImportacionesControllerTest
    {
        private Mock<ILogicaProyecto> _logicaP;
        private Mock<IMapper> _mapper;
        private ImportacionesController _iController;

        [SetUp]
        public void Setup()
        {
            _logicaP = new Mock<ILogicaProyecto>();
            _mapper = new Mock<IMapper>();
            _iController = new ImportacionesController(_logicaP.Object, _mapper.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _logicaP = null;
            _mapper = null;
            _iController = null;
        }

        [Test]
        public void se_pueden_importar_bugs()
        {
            _logicaP.Setup(c => c.ImportarBugs(It.IsAny<string>()));

            var result = _iController.Post("ruta");

            Assert.IsNotNull(result);

            _logicaP.Verify(c => c.ImportarBugs(It.IsAny<string>()));
        }
    }
}
