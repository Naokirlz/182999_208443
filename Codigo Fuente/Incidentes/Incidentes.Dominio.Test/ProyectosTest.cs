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
                ProyectoId = 1,
                EstadoIncidente = Incidente.Estado.Activo,
                Descripcion = "Descripcion del incidente",
                Version = "1"
            };
            Incidente otroIncidente = new Incidente()
            {
                Nombre = "Incidente 2",
                ProyectoId = 1,
                EstadoIncidente = Incidente.Estado.Activo,
                Descripcion = "Descripcion del incidente 2",
                Version = "1"
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
        public void se_puede_asignar_tareas_a_un_proyecto()
        {
            Tarea unaTarea = new Tarea()
            {
                Nombre = "Tarea 1",
                Costo = 1,
                Duracion = 1
            };
            Tarea otraTarea = new Tarea()
            {
                Nombre = "Tarea 2",
                Costo = 1,
                Duracion = 1
            };
            List<Tarea> lista = new List<Tarea>();
            lista.Add(unaTarea);
            lista.Add(otraTarea);

            Proyecto unProyecto = new Proyecto()
            {
                Nombre = "Trabajo conjunto",
                Tareas = lista
            };
            Assert.AreEqual(2, unProyecto.Tareas.Count());
        }

        [Test]
        public void se_puede_asignar_desarrolladores_a_un_proyecto()
        {
            Usuario unDesarrollador = new Usuario()
            {
                Nombre = "Luisito",
                Apellido = "Gomez",
                Contrasenia = "123456789",
                Email = "luisito@gmail.com",
                RolUsuario = Usuario.Rol.Desarrollador,
                Id = 1,
                NombreUsuario = "luisito123"
            };
            Usuario otroDesarrollador = new Usuario()
            {
                Nombre = "Luisito2",
                Apellido = "Gomez",
                Contrasenia = "123456789",
                Email = "luisito2@gmail.com",
                RolUsuario = Usuario.Rol.Desarrollador,
                Id = 2,
                NombreUsuario = "luisito1234"
            };
            List<Usuario> lista = new List<Usuario>();
            lista.Add(unDesarrollador);
            lista.Add(otroDesarrollador);

            Proyecto unProyecto = new Proyecto()
            {
                Nombre = "Trabajo conjunto",
                Asignados = lista
            };
            Assert.AreEqual(2, unProyecto.Asignados.Count());
        }

        [Test]
        public void se_puede_asignar_testers_a_un_proyecto()
        {
            Usuario unTester = new Usuario()
            {
                Nombre = "Luisito",
                Apellido = "Gomez",
                Contrasenia = "123456789",
                Email = "luisito@gmail.com",
                Id = 1,
                RolUsuario = Usuario.Rol.Tester,
                NombreUsuario = "luisito123"
            };
            Usuario otroTester = new Usuario()
            {
                Nombre = "Luisito2",
                Apellido = "Gomez",
                Contrasenia = "123456789",
                Email = "luisito2@gmail.com",
                RolUsuario = Usuario.Rol.Tester,
                Id = 2,
                NombreUsuario = "luisito1234"
            };
            List<Usuario> lista = new List<Usuario>();
            lista.Add(unTester);
            lista.Add(otroTester);

            Proyecto unProyecto = new Proyecto()
            {
                Nombre = "Trabajo conjunto",
                Asignados = lista
            };
            Assert.AreEqual(2, unProyecto.Asignados.Count());
        }
    }
}
