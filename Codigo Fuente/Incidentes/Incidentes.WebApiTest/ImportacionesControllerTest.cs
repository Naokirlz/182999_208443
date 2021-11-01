using Incidentes.Logica.DTOs;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Moq;
using NUnit.Framework;

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
    }
}
