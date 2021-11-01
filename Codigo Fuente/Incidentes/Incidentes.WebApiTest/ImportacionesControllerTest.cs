using Incidentes.Logica.DTOs;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.WebApiTest
{
    public class ImportacionesControllerTest
    {
        private Mock<ILogicaImportaciones> _logicaI;
        private ImportacionesController _iController;

        [SetUp]
        public void Setup()
        {
            _logicaI = new Mock<ILogicaImportaciones>();
            _iController = new ImportacionesController(_logicaI.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _logicaI = null;
            _iController = null;
        }

        [Test]
        public void se_pueden_importar_bugs()
        {
            _logicaI.Setup(c => c.ImportarBugs(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()));
            FuenteDTO fuente = new FuenteDTO() { 
                rutaFuente = "ruta",
                rutaBinario = "ruta",
                usuarioId = 5
            };
            var result = _iController.Post(fuente);

            Assert.IsNotNull(result);

            _logicaI.Verify(c => c.ImportarBugs(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()));
        }

        [Test]
        public void se_pueden_ver_una_lista_de_dll_de_importaciones()
        {
            _logicaI.Setup(c => c.ListarPlugins()).Returns(new List<string>() { "uno", "dos"});
            var result = _iController.Get();
            var okResult = result as OkObjectResult;
            var resp = (List<string>)okResult.Value;

            Assert.AreEqual(2, resp.Count());
            _logicaI.Verify(c => c.ListarPlugins());
        }
    }
}
