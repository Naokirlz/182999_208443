using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.Interfaz;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Incidentes.Logica.Test
{
    public class GestorIncidenteTest
    {
        private Incidente incidente;
        Mock<IRepositorioGestores> repoGestores;
        GestorIncidente gestorIncidente;
        private Usuario usuarioCompleto;
        IQueryable<Usuario> queryableUsuarios;
        List<Incidente> listaI;
        List<Usuario> listaU;
        IQueryable<Incidente> incidentes;

        [SetUp]
        public void Setup()
        {
             incidente = new Incidente()
            {
                Nombre = "incidente01",
                Descripcion = "IncidentePrueba",
                Id=1
            };

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
            gestorIncidente = new GestorIncidente(repoGestores.Object);

            repoGestores.Setup(c => c.RepositorioIncidente.Alta(incidente));

            listaI = new List<Incidente>();
            listaU = new List<Usuario>();
            queryableUsuarios = listaU.AsQueryable();
            incidentes = listaI.AsQueryable();

            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioIncidente.ObtenerTodos(false)).Returns(incidentes);
            repoGestores.Setup(c => c.RepositorioIncidente.Eliminar(incidente));
        }

        [TearDown]
        public void TearDown()
        {
            incidente = null;
            repoGestores = null;
            gestorIncidente = null;
            this.usuarioCompleto = null;
            queryableUsuarios = null;
            listaI = null;
            listaU = null;
            incidentes = null;
        }


        [Test]
        public void se_puede_guardar_incidente()
        {
            Incidente inc01 = gestorIncidente.Alta(incidente);

            Assert.AreEqual(incidente.Nombre, inc01.Nombre);
            repoGestores.Verify(c => c.RepositorioIncidente.Alta(incidente));
        }

        [Test]
        public void un_usuario_logueado_puede_ver_incidentes()
        {
            
            Incidente inc1 = new Incidente();
            Incidente inc2 = new Incidente();
            listaI.Add(inc1);
            listaI.Add(inc2);
            listaU.Add(usuarioCompleto);
            

            incidentes = (IQueryable<Incidente>)gestorIncidente.ObtenerTodos();

            Assert.AreEqual(2, incidentes.Count());
            repoGestores.Verify(c => c.RepositorioIncidente.ObtenerTodos(false));
        }

        [Test]
        public void se_puede_ver_un_incidente()
        {
            Incidente incidente02 = gestorIncidente.Alta(incidente);

            Assert.AreEqual(incidente.Nombre, incidente02.Nombre);
            repoGestores.Verify(c => c.RepositorioIncidente.Alta(incidente));
        }

        [Test]
        public void alta_devuelve_una_instancia_de_incidente()
        {
            
            Incidente incidente02 = gestorIncidente.Alta(incidente);
            Assert.IsNotNull(incidente02);
            repoGestores.Verify(c => c.RepositorioIncidente.Alta(incidente));
        }

        [Test]
        public void se_puede_modificar_un_incidente()
        {
        }

        [Test]
        public void modificar_devuelve_una_instancia_de_incidente()
        {
        }

        [Test]
        public void se_puede_eliminar_un_incidente()
        {
            gestorIncidente.Alta(incidente);

            incidentes = (IQueryable<Incidente>)gestorIncidente.ObtenerTodos();
            Assert.AreEqual(1, incidentes.Count());

            gestorIncidente.Baja(incidente.Id);

            incidentes = (IQueryable<Incidente>)gestorIncidente.ObtenerTodos();
            Assert.AreEqual(0, incidentes.Count());

            repoGestores.Verify(c => c.RepositorioIncidente.Alta(incidente));
            repoGestores.Verify(c => c.RepositorioIncidente.Eliminar(incidente));
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
            repoGestores.Verify(c => c.RepositorioIncidente.ObtenerTodos(false));

        }
    }
}
