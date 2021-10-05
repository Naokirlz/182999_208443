using Incidentes.Dominio;
using Incidentes.LogicaInterfaz;
using Incidentes.WebApi.Controllers;
using Moq;
using NUnit.Framework;

namespace Incidentes.WebApiTest
{
    public class LoginControllerTest
    {
        private Mock<ILogicaUsuario> _logicaU;
        private LoginController _lController;

        [SetUp]
        public void Setup()
        {
            _logicaU = new Mock<ILogicaUsuario>();
            _lController = new LoginController(_logicaU.Object);
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

        [Test]
        public void se_pueden_desloguear_los_usuarios()
        {
            Usuario user = new Usuario()
            {
                Token = "asdadsacasc"
            };
            _logicaU.Setup(c => c.Logout(It.IsAny<string>()));

            var result = _lController.Logout(user);

            Assert.IsNotNull(result);

            _logicaU.VerifyAll();
        }
    }
}
