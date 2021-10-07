using NUnit.Framework;
using Incidentes.Dominio;

namespace Incidentes.Dominio.Test
{
    public class IncidenteTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void se_puede_dar_nombre_a_un_incidente()
        {
            Incidente unIncidente = new Incidente()
            {
                Nombre = "Incidente complicado"
            };
            Assert.AreEqual("Incidente complicado", unIncidente.Nombre);
        }

        [Test]
        public void se_puede_asignar_a_un_incidente_un_proyecto()
        {
            Incidente unIncidente = new Incidente()
            {
                ProyectoId = 1
            };
            Assert.AreEqual(1, unIncidente.ProyectoId);
        }

        [Test]
        public void se_puede_dar_descripcion_a_un_incidente()
        {
            Incidente unIncidente = new Incidente()
            {
                Descripcion = "descripcion de incidente"
            };
            Assert.AreEqual("descripcion de incidente", unIncidente.Descripcion);
        }
        [Test]
        public void se_puede_dar_version_a_un_incidente()
        {
            Incidente unIncidente = new Incidente()
            {
                Version = "1"
            };
            Assert.AreEqual("1", unIncidente.Version);
        }

        [Test]
        public void se_incidente_puede_estar_activo()
        {
            Incidente unIncidente = new Incidente()
            {
                EstadoIncidente = Incidente.Estado.Activo
            };
            Assert.AreEqual(Incidente.Estado.Activo, unIncidente.EstadoIncidente);
        }

        [Test]
        public void se_incidente_puede_estar_resuelto()
        {
            Incidente unIncidente = new Incidente()
            {
                EstadoIncidente = Incidente.Estado.Resuelto
            };
            Assert.AreEqual(Incidente.Estado.Resuelto, unIncidente.EstadoIncidente);
        }

        [Test]
        public void se_incidente_puede_ser_resuelto_por_un_desarrollador()
        {
            Usuario unDesarrollador = new Usuario()
            {
                Nombre = "Luisito",
                Apellido = "Gomez",
                Contrasenia = "123456789",
                Email = "luisito@gmail.com",
                Id = 1,
                NombreUsuario = "luisito123"
            };
            Incidente unIncidente = new Incidente()
            {
                Nombre = "Nombre incidente",
                DesarrolladorId = unDesarrollador.Id
            };
            Assert.AreEqual(unDesarrollador.Id, unIncidente.DesarrolladorId);
        }
    }
}
