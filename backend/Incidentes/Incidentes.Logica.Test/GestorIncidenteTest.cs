﻿using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.Excepciones;
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

            this.usuarioCompleto = new Usuario()
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
            listaI = new List<Incidente>();
            listaU = new List<Usuario>();
            queryableUsuarios = listaU.AsQueryable();
            incidentes = listaI.AsQueryable();
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
            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(false);

            Incidente incidente02 = gestorIncidente.Alta(incidente);
            Assert.IsNotNull(incidente02);
            repoGestores.Verify(c => c.RepositorioIncidente.Alta(incidente));
        }

        [Test]
        public void se_puede_modificar_un_incidente()
        {
        }

        [Test]
        public void se_puede_eliminar_un_incidente()
        {
            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(true);

            gestorIncidente.Baja(5);
            IQueryable<Incidente> incidentes = (IQueryable<Incidente>)gestorIncidente.ObtenerTodos();
            Assert.AreEqual(0, incidentes.Count());
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
            repoGestores.Verify(c => c.RepositorioIncidente.ObtenerTodos(false));

        }

        [Test]
        public void no_se_puede_modificar_un_incidente_que_no_existe()
        {
            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(false);

            Assert.Throws<ExcepcionElementoNoExiste>(() => gestorIncidente.Modificar(20, new Incidente()));

            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
        }

        [Test]
        public void no_se_puede_eliminar_un_incidente_que_no_existe()
        {
            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(false);

            Assert.Throws<ExcepcionElementoNoExiste>(() => gestorIncidente.Baja(20));

            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
        }

        [Test]
        public void no_se_puede_crear_un_incidente_con_nombre_corto()
        {
            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(false);
            Assert.Throws<ExcepcionLargoTexto>(() => gestorIncidente.Alta(new Incidente()
            {
                Nombre = "ae"
            }));
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
        }

        [Test]
        public void no_se_puede_crear_un_incidente_con_nombre_largo()
        {
            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(false);
            Assert.Throws<ExcepcionLargoTexto>(() => gestorIncidente.Alta(new Incidente()
            {
                Nombre = "zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz"
            }));
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
        }

        [Test]
        public void se_puede_ver_la_lista_de_bugs_de_los_proyectos_que_pertenece()
        {
            List<Incidente> lista = new List<Incidente>();
            lista.Add(new Incidente());

            List<Usuario> listaU = new List<Usuario>();
            listaU.Add(new Usuario());
            IQueryable<Usuario> queryableU = listaU.AsQueryable();

            repoGestores.Setup(
                c => c.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Incidente>()))
                .Returns(lista);

            List<Incidente> incidentes = gestorIncidente.ListaDeIncidentesDeLosProyectosALosQuePertenece(usuarioCompleto.Id, "", new Incidente());

            Assert.AreEqual(1, lista.Count());
            repoGestores.Verify(
                c => c.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Incidente>()));
        }

        [Test]
        public void se_puede_ver_un_incidente_de_un_usuario()
        {
            Incidente incidenteD = new Incidente()
            {
                Id = 2,
                Nombre = "Incidente"
            };
            List<Incidente> lista = new List<Incidente>();
            lista.Add(incidenteD);
            IQueryable<Incidente> queryableI = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioProyecto.VerificarIncidentePerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), true)).Returns(queryableI);

            Incidente encontrado = gestorIncidente.ObtenerParaUsuario(1, 2);

            Assert.AreEqual(incidenteD.Nombre, encontrado.Nombre);
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
            repoGestores.Verify(c => c.RepositorioProyecto.VerificarIncidentePerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
            repoGestores.Verify(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
            repoGestores.Verify(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), true));
        }

        [Test]
        public void no_se_puede_ver_si_usuario_no_pertenece_a_proyecto()
        {
            Incidente incidenteD = new Incidente()
            {
                Id = 2,
                Nombre = "Incidente"
            };
            List<Incidente> lista = new List<Incidente>();
            lista.Add(incidenteD);
            IQueryable<Incidente> queryableI = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), true)).Returns(queryableI);
            repoGestores.Setup(c => c.RepositorioProyecto.VerificarIncidentePerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            Assert.Throws<ExcepcionAccesoNoAutorizado>(() => gestorIncidente.ObtenerParaUsuario(1, 2));
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
            repoGestores.Verify(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), true));
            repoGestores.Verify(c => c.RepositorioProyecto.VerificarIncidentePerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
            repoGestores.Verify(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void no_se_puede_ver_si_incidente_no_pertenece_a_proyecto()
        {
            Incidente incidenteD = new Incidente()
            {
                Id = 2,
                Nombre = "Incidente"
            };
            List<Incidente> lista = new List<Incidente>();
            lista.Add(incidenteD);
            IQueryable<Incidente> queryableI = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), true)).Returns(queryableI);
            repoGestores.Setup(c => c.RepositorioProyecto.VerificarIncidentePerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            Assert.Throws<ExcepcionAccesoNoAutorizado>(() => gestorIncidente.ObtenerParaUsuario(1, 2));
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
            repoGestores.Verify(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), true));
            repoGestores.Verify(c => c.RepositorioProyecto.VerificarIncidentePerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }

    }
}
