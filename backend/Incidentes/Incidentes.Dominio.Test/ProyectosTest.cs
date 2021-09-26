using NUnit.Framework;
using Incidentes.Dominio;
using System.Collections.Generic;
using System.Linq;

namespace Incidentes.Dominio.Test
{
    public class ProyectosTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void se_puede_dar_nombre_a_un_proyecto()
        {
            Proyecto unProyecto = new Proyecto()
            {
                Nombre = "Trabajo conjunto"
            };
            Assert.AreEqual("Trabajo conjunto", unProyecto.Nombre);
        }

        [Test]
        public void se_puede_dar_id_a_un_proyecto()
        {
            Proyecto unProyecto = new Proyecto()
            {
                Id = 1
            };
            Assert.AreEqual(1, unProyecto.Id);
        }

        [Test]
        public void se_puede_asignar_incidentes_a_un_proyecto()
        {
            Incidente unIncidente = new Incidente() { 
                Nombre = "Incidente 1",
                NombreProyecto = "Trabajo conjunto",
                EstadoIncidente = Incidente.Estado.Activo,
                Descripcion = "Descripcion del incidente",
                Version = 1
            };
            Incidente otroIncidente = new Incidente()
            {
                Nombre = "Incidente 2",
                NombreProyecto = "Trabajo conjunto",
                EstadoIncidente = Incidente.Estado.Activo,
                Descripcion = "Descripcion del incidente 2",
                Version = 1
            };
            List<Incidente> lista = new List<Incidente>();
            lista.Add(unIncidente);
            lista.Add(otroIncidente);

            Proyecto unProyecto = new Proyecto()
            {
                Nombre = "Trabajo conjunto",
                Incidentes = lista
            };
            Assert.AreEqual(2, unProyecto.Incidentes.Count());
        }

        [Test]
        public void se_puede_asignar_desarrolladores_a_un_proyecto()
        {
            Desarrollador unDesarrollador = new Desarrollador()
            {
                Nombre = "Luisito",
                Apellido = "Gomez",
                Contrasenia = "123456789",
                Email = "luisito@gmail.com",
                Id= 1,
                NombreUsuario = "luisito123"
            };
            Desarrollador otroDesarrollador = new Desarrollador()
            {
                Nombre = "Luisito2",
                Apellido = "Gomez",
                Contrasenia = "123456789",
                Email = "luisito2@gmail.com",
                Id = 2,
                NombreUsuario = "luisito1234"
            };
            List<Desarrollador> lista = new List<Desarrollador>();
            lista.Add(unDesarrollador);
            lista.Add(otroDesarrollador);

            Proyecto unProyecto = new Proyecto()
            {
                Nombre = "Trabajo conjunto",
                Desarrolladores = lista
            };
            Assert.AreEqual(2, unProyecto.Desarrolladores.Count());
        }

        [Test]
        public void se_puede_asignar_testers_a_un_proyecto()
        {
            Tester unTester = new Tester()
            {
                Nombre = "Luisito",
                Apellido = "Gomez",
                Contrasenia = "123456789",
                Email = "luisito@gmail.com",
                Id = 1,
                NombreUsuario = "luisito123"
            };
            Tester otroTester = new Tester()
            {
                Nombre = "Luisito2",
                Apellido = "Gomez",
                Contrasenia = "123456789",
                Email = "luisito2@gmail.com",
                Id = 2,
                NombreUsuario = "luisito1234"
            };
            List<Tester> lista = new List<Tester>();
            lista.Add(unTester);
            lista.Add(otroTester);

            Proyecto unProyecto = new Proyecto()
            {
                Nombre = "Trabajo conjunto",
                Testers = lista
            };
            Assert.AreEqual(2, unProyecto.Testers.Count());
        }
    }
}
