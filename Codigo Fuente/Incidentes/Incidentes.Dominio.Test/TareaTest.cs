using NUnit.Framework;
using Incidentes.Dominio;

namespace Incidentes.Dominio.Test
{
    public class TareaTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void se_puede_dar_nombre_a_una_tarea()
        {
            Tarea unaTarea = new Tarea()
            {
                Nombre = "Tarea complicada"
            };
            Assert.AreEqual("Tarea complicada", unaTarea.Nombre);
        }

        [Test]
        public void se_puede_dar_duracion_a_una_tarea()
        {
            Tarea unaTarea = new Tarea()
            {
                Duracion = 2
            };
            Assert.AreEqual(2, unaTarea.Duracion);
        }

        [Test]
        public void se_puede_dar_costo_a_una_tarea()
        {
            Tarea unaTarea = new Tarea()
            {
                Costo = 2
            };
            Assert.AreEqual(2, unaTarea.Costo);
        }

        [Test]
        public void se_puede_dar_id_a_una_tarea()
        {
            Tarea unaTarea = new Tarea()
            {
                Id = 2
            };
            Assert.AreEqual(2, unaTarea.Id);
        }

        [Test]
        public void se_puede_asignar_una_tarea_a_un_proyecto()
        {
            Tarea unaTarea = new Tarea()
            {
                ProyectoId = 2
            };
            Assert.AreEqual(2, unaTarea.ProyectoId);
        }
    }
}
