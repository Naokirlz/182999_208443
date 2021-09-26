using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Moq;
using NUnit.Framework;

namespace Incidentes.Logica.Test
{
    public class GestorAdministradorTest
    {
        [SetUp]
        public void Setup()
        {
           //logica = new Gr();
        }

        [Test]
        public void se_puede_guardar_administrador()
        {
            Administrador administrador = new Administrador()
            {
                Nombre = "Luisito"
            };    

            Mock<IRepositorioGestores> repoGestores = new Mock<IRepositorioGestores>();

            repoGestores.Setup(c => c.RepositorioAdministrador.Crear(administrador));

            GestorAdministrador gestor = new GestorAdministrador(repoGestores.Object);

            Administrador admin = gestor.Alta(administrador);

            Assert.AreEqual(administrador.Nombre, admin.Nombre);
            repoGestores.Verify(c => c.RepositorioAdministrador.Crear(administrador));
        }

        
    }
}