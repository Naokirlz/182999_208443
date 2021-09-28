using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.Excepciones;
using Incidentes.Logica.Interfaz;
using Moq;
using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Incidentes.Logica.Test
{
    public class GestorProyectoTest
    {
        private Usuario usuarioCompleto;
        private Proyecto unProyecto;
        Mock<IRepositorioGestores> repoGestores;
        GestorUsuario gestorUsuario;
        GestorProyecto gestorProyecto;

        [SetUp]
        public void SetUp()
        {
            this.usuarioCompleto = new Administrador()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                Email = "martin@gmail.com",
                Id = 1,
                NombreUsuario = "martincosa",
                Token = ""
            };

            unProyecto = new Proyecto() { 
                Nombre = "Proyecto"
            };

            repoGestores = new Mock<IRepositorioGestores>();
            gestorUsuario = new GestorUsuario(repoGestores.Object);
            gestorProyecto = new GestorProyecto(repoGestores.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this.usuarioCompleto = null;
            repoGestores = null;
            gestorUsuario = null;
            gestorProyecto = null;
            unProyecto = null;
        }

        [Test]
        public void se_puede_guardar_proyecto()
        {
            Proyecto proyecto = new Proyecto()
            {
                Nombre = "Proyecto1"
            };

            repoGestores.Setup(c => c.RepositorioProyecto.Alta(proyecto));

            Proyecto proyecto1 = gestorProyecto.Alta(proyecto);

            Assert.AreEqual(proyecto.Nombre, proyecto1.Nombre);
            repoGestores.Verify(c => c.RepositorioProyecto.Alta(proyecto));
        }

        [Test]
        public void un_usuario_logueado_puede_ver_proyectos()
        {
            List<Proyecto> lista = new List<Proyecto>();
            Proyecto pro1 = new Proyecto();
            Proyecto pro2 = new Proyecto();
            lista.Add(pro1);
            lista.Add(pro2);
            IQueryable<Proyecto> queryableProyectos = lista.AsQueryable();

            List<Usuario> lista2 = new List<Usuario>();
            lista2.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista2.AsQueryable();

            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerTodos(false)).Returns(queryableProyectos);

            IQueryable<Proyecto> proyectos = (IQueryable<Proyecto>)gestorProyecto.ObtenerTodos();

            Assert.AreEqual(2, proyectos.Count());
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerTodos(false));
        }

        [Test]
        public void se_puede_ver_un_proyecto()
        {
            Proyecto proyecto = new Proyecto()
            {
                Nombre = "Proyecto1"
            };

            repoGestores.Setup(c => c.RepositorioProyecto.Alta(proyecto));

            Proyecto proyecto1 = gestorProyecto.Alta(proyecto);

            Assert.AreEqual(proyecto.Nombre, proyecto1.Nombre);
            repoGestores.Verify(c => c.RepositorioProyecto.Alta(proyecto));
        }

        [Test]
        public void alta_devuelve_una_instancia_de_proyecto()
        {

        }

        [Test]
        public void se_puede_modificar_un_proyecto()
        {
        }

        [Test]
        public void modificar_devuelve_una_instancia_de_proyecto()
        {
        }

        [Test]
        public void se_puede_eliminar_un_proyecto()
        {
        }

        [Test]
        public void no_se_puede_guardar_un_proyecto_con_nombre_repetido()
        {
        }

        [Test]
        public void no_se_puede_modificar_un_proyecto_que_no_existe()
        {
        }

        [Test]
        public void no_se_puede_eliminar_un_proyecto_que_no_existe()
        {
        }
    }
}
