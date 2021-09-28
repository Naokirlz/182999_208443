using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Moq;
using NUnit.Framework;

namespace Incidentes.Logica.Test
{
    public class GestorIncidenteTest
    {
        [SetUp]
        public void Setup()
        {
            //logica = new Gr();
        }

        [Test]
        public void se_puede_guardar_incidente()
        {
            Incidente incidente = new Incidente()
            {
                Nombre = "incidente01"
            };

            Mock<IRepositorioGestores> repoGestores = new Mock<IRepositorioGestores>();

            repoGestores.Setup(c => c.RepositorioIncidente.Alta(incidente));

            GestorIncidente gestor = new GestorIncidente(repoGestores.Object);

            Incidente inc01 = gestor.Alta(incidente);

            Assert.AreEqual(incidente.Nombre, inc01.Nombre);
            repoGestores.Verify(c => c.RepositorioIncidente.Alta(incidente));
        }
    }
}
