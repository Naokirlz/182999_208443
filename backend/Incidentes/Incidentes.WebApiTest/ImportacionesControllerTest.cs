using Incidentes.Logica.DTOs;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Moq;
using NUnit.Framework;

namespace Incidentes.WebApiTest
{
    public class ImportacionesControllerTest
    {
        private Mock<ILogicaProyecto> _logicaP;
        private ImportacionesController _iController;

        [SetUp]
        public void Setup()
        {
            _logicaP = new Mock<ILogicaProyecto>();
            _iController = new ImportacionesController(_logicaP.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _logicaP = null;
            _iController = null;
        }

        [Test]
        public void se_pueden_importar_bugs()
        {
            _logicaP.Setup(c => c.ImportarBugs(It.IsAny<string>(), It.IsAny<int>()));
            FuenteDTO fuente = new FuenteDTO() { 
                rutaFuente = "ruta",
                usuarioId = 5
            };
            var result = _iController.Post(fuente);

            Assert.IsNotNull(result);

            _logicaP.Verify(c => c.ImportarBugs(It.IsAny<string>(), It.IsAny<int>()));
        }
    }
}
