using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Moq;
using NUnit.Framework;

namespace Incidentes.Logica.Test
{
    public class GestorProyectoTest
    {
        [SetUp]
        public void Setup()
        {
            //logica = new Gr();
        }

        [Test]
        public void se_puede_guardar_proyecto()
        {
            Proyecto proyecto = new Proyecto()
            {
                Nombre = "Proyecto1"
            };

            Mock<IRepositorioGestores> repoGestores = new Mock<IRepositorioGestores>();

            repoGestores.Setup(c => c.RepositorioProyecto.Alta(proyecto));

            GestorProyecto gestor = new GestorProyecto(repoGestores.Object);

            Proyecto proyecto1 = gestor.Alta(proyecto);

            Assert.AreEqual(proyecto.Nombre, proyecto1.Nombre);
            repoGestores.Verify(c => c.RepositorioProyecto.Alta(proyecto));
        }
    }
}
