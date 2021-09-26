using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Moq;
using NUnit.Framework;

namespace Incidentes.Logica.Test
{
    public class GestorUsuarioTest
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

            repoGestores.Setup(c => c.RepositorioUsuario.Crear(administrador));

            GestorUsuario gestor = new GestorUsuario(repoGestores.Object);

            Usuario admin = gestor.Alta(administrador);

            Assert.AreEqual(administrador.Nombre, admin.Nombre);
            repoGestores.Verify(c => c.RepositorioUsuario.Crear(administrador));
        }

        
    }
}