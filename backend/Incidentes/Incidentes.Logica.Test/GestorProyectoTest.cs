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
        public void un_usuario_no_logueado_no_puede_ver_proyectos()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            Assert.Throws<ExcepcionAccesoNoAutorizado>(() => gestorProyecto.ObtenerTodos(""));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
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

            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);
            repoGestores.Setup(c => c.RepositorioUsuario.ListaDeProyectosALosQuePertenece(It.IsAny<int>())).Returns(queryableProyectos);
            
            IQueryable<Proyecto> proyectos = (IQueryable<Proyecto>)gestorProyecto.ObtenerTodos(usuarioCompleto.Token);

            Assert.AreEqual(2, proyectos.Count());
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioUsuario.ListaDeProyectosALosQuePertenece(It.IsAny<int>()));
        }
    }
}
