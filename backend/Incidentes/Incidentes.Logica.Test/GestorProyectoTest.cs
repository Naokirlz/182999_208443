﻿using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.Excepciones;
using Incidentes.Logica.Interfaz;
using Moq;
using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.IO;

namespace Incidentes.Logica.Test
{
    public class GestorProyectoTest
    {
        private Usuario usuarioCompleto;
        private Proyecto unProyecto;
        Mock<IRepositorioGestores> repoGestores;
        GestorProyecto gestorProyecto;
        GestorIncidente gestorIncidente;

        [SetUp]
        public void SetUp()
        {
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

            unProyecto = new Proyecto() { 
                Nombre = "Proyecto"
            };

            repoGestores = new Mock<IRepositorioGestores>();
            gestorProyecto = new GestorProyecto(repoGestores.Object);
            gestorIncidente = new GestorIncidente(repoGestores.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this.usuarioCompleto = null;
            repoGestores = null;
            gestorProyecto = null;
            gestorIncidente = null;
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
            Proyecto proyectoD = new Proyecto()
            {
                Id = 2,
                Nombre = "Proyecto1"
            };
            List<Proyecto> lista = new List<Proyecto>();
            lista.Add(proyectoD);
            IQueryable<Proyecto> queryableP = lista.AsQueryable();
 
            repoGestores.Setup(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true)).Returns(queryableP);

            Proyecto encontrado = gestorProyecto.Obtener(2);

            Assert.AreEqual(proyectoD.Nombre, encontrado.Nombre);
            repoGestores.Verify(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>()));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true));
        }

        [Test]
        public void se_puede_ver_un_proyecto_de_un_usuario()
        {
            Proyecto proyectoD = new Proyecto()
            {
                Id = 2,
                Nombre = "Proyecto1"
            };
            List<Proyecto> lista = new List<Proyecto>();
            lista.Add(proyectoD);
            IQueryable<Proyecto> queryableP = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true)).Returns(queryableP);

            Proyecto encontrado = gestorProyecto.ObtenerParaUsuario(1, 2);

            Assert.AreEqual(proyectoD.Nombre, encontrado.Nombre);
            repoGestores.Verify(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>()));
            repoGestores.Verify(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true));
        }

        [Test]
        public void no_se_puede_ver_si_usuario_no_pertenece_a_proyecto()
        {
            repoGestores.Setup(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            Assert.Throws<ExcepcionAccesoNoAutorizado>(() => gestorProyecto.ObtenerParaUsuario(1, 2));
            repoGestores.Verify(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Test]
        public void se_puede_modificar_un_proyecto()
        {
            Proyecto proyecto = new Proyecto()
            {
                Id = 3,
                Nombre = "Proyecto1"
            };
            List<Proyecto> lista = new List<Proyecto>();
            lista.Add(proyecto);
            IQueryable<Proyecto> queryableP = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioProyecto.Modificar(proyecto));
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true)).Returns(queryableP);

            Proyecto proyecto1 = gestorProyecto.Modificar(3, proyecto);
            Proyecto encontrado = gestorProyecto.Obtener(3);

            Assert.AreEqual(proyecto.Nombre, encontrado.Nombre);
            repoGestores.Verify(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>()));
            repoGestores.Verify(c => c.RepositorioProyecto.Modificar(proyecto));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true));
        }

        [Test]
        public void se_pueden_asignar_usuarios_a_un_proyecto()
        {
            Proyecto proyecto = new Proyecto()
            {
                Id = 3,
                Nombre = "Proyecto1"
            };
            List<Proyecto> lista = new List<Proyecto>();
            lista.Add(proyecto);
            IQueryable<Proyecto> queryableP = lista.AsQueryable();

            List<Usuario> listaU = new List<Usuario>();
            listaU.Add(new Usuario());
            IQueryable<Usuario> queryableU = listaU.AsQueryable();

            repoGestores.Setup(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioProyecto.Modificar(It.IsAny<Proyecto>()));
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true)).Returns(queryableP);
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), true)).Returns(queryableU);

            List<int> listaInt = new List<int>();
            listaInt.Add(2);
            listaInt.Add(5);
            listaInt.Add(8);

            gestorProyecto.AgregarDesarrolladorAProyecto(listaInt, proyecto.Id);

            Assert.AreEqual(3, proyecto.Asignados.Count());
            repoGestores.Verify(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>()));
            repoGestores.Verify(c => c.RepositorioProyecto.Modificar(It.IsAny<Proyecto>()));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true));
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), true));
        }

        [Test]
        public void se_puede_verificar_si_un_usuario_pertence_a_un_proyecto()
        {
            repoGestores.Setup(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(1, 1)).Returns(true);

            bool pertenece = gestorProyecto.VerificarUsuarioPerteneceAlProyecto(1, 1);

            Assert.IsTrue(pertenece);
            repoGestores.Verify(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(1, 1));
        }

        [Test]
        public void no_se_puede_dar_alta_un_proyecto_nulo()
        {
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestorProyecto.Alta(null));
        }

        [Test]
        public void no_se_puede_modificar_un_proyecto_nulo()
        {
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestorProyecto.Alta(null));
        }

        [Test]
        public void se_puede_eliminar_un_proyecto()
        {
            IQueryable<Proyecto> queryableP = new List<Proyecto>().AsQueryable();

            repoGestores.Setup(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerTodos(false)).Returns(queryableP);

            gestorProyecto.Baja(3);
            IQueryable<Proyecto> proyectos = (IQueryable<Proyecto>)gestorProyecto.ObtenerTodos();
            Assert.AreEqual(0, proyectos.Count());
            repoGestores.Verify(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>()));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerTodos(false));
        }

        [Test]
        public void no_se_puede_guardar_un_proyecto_con_nombre_repetido()
        {
            Proyecto proyecto = new Proyecto()
            {
                Nombre = "Proyecto"
            };

            repoGestores.Setup(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>())).Returns(true);

            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestorProyecto.Alta(proyecto));
            repoGestores.Verify(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>()));
        }

        [Test]
        public void no_se_puede_modificar_un_proyecto_que_no_existe()
        {
            repoGestores.Setup(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>())).Returns(false);

            Assert.Throws<ExcepcionElementoNoExiste>(() => gestorProyecto.Modificar(3, new Proyecto()));

            repoGestores.Verify(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>()));
        }

        [Test]
        public void no_se_puede_eliminar_un_proyecto_que_no_existe()
        {
            repoGestores.Setup(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>())).Returns(false);

            Assert.Throws<ExcepcionElementoNoExiste>(() => gestorProyecto.Baja(3));

            repoGestores.Verify(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>()));
        }

        [Test]
        public void no_se_puede_crear_un_proyecto_con_nombre_corto()
        {
            repoGestores.Setup(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>())).Returns(false);
            Assert.Throws<ExcepcionLargoTexto>(() => gestorProyecto.Alta(new Proyecto() { 
                Nombre = "1234"
            }));
            repoGestores.Verify(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>()));
        }

        [Test]
        public void no_se_puede_crear_un_proyecto_con_nombre_largo()
        {
            repoGestores.Setup(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>())).Returns(false);
            Assert.Throws<ExcepcionLargoTexto>(() => gestorProyecto.Alta(new Proyecto()
            {
                Nombre = "12345678901234567890123456"
            }));
            repoGestores.Verify(c => c.RepositorioProyecto.Existe(It.IsAny<Expression<Func<Proyecto, bool>>>()));
        }

        [Test]
        public void se_pueden_cargar_incidentes_a_un_proyecto_con_xml()
        {
            string rutaFuenteXML = AppDomain.CurrentDomain.BaseDirectory + "\\Fuentes\\Fuente.xml";
            
            Proyecto proyecto = new Proyecto()
            {
                Id = 3,
                Nombre = "Proyecto1"
            };
            List<Proyecto> lista = new List<Proyecto>();
            for (int i = 0; i < 2; i++)
                proyecto.Incidentes.Add(new Incidente());
            lista.Add(proyecto);
            IQueryable<Proyecto> queryableP = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true)).Returns(queryableP);
            repoGestores.Setup(c => c.Save());
            repoGestores.Setup(
                c => c.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Incidente>()))
                .Returns(proyecto.Incidentes);
            repoGestores.Setup(c => c.RepositorioIncidente.Alta(It.IsAny<Incidente>()));


            gestorProyecto.ImportarBugs(rutaFuenteXML);

            int incidentes = gestorIncidente.ListaDeIncidentesDeLosProyectosALosQuePertenece(1, "proyecto", new Incidente()).Count();

            Assert.AreEqual(4, incidentes);
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true));
            repoGestores.Verify(c => c.Save());
            repoGestores.Verify(c => c.RepositorioIncidente.Alta(It.IsAny<Incidente>()));
            repoGestores.Verify(
                c => c.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Incidente>()));
        }

        [Test]
        public void no_se_pueden_cargar_incidentes_a_un_proyecto_si_no_existe_archivo_xml()
        {
            string rutaFuenteXML = AppDomain.CurrentDomain.BaseDirectory + "\\Fuentes\\NoExiste.xml";
            Assert.Throws<ExcepcionElementoNoExiste>(() => gestorProyecto.ImportarBugs(rutaFuenteXML));
        }

        [Test]
        public void se_pueden_cargar_incidentes_a_un_proyecto_con_texto()
        {
            string rutaFuenteTXT = AppDomain.CurrentDomain.BaseDirectory + "\\Fuentes\\Fuente.txt";

            Proyecto proyecto = new Proyecto()
            {
                Id = 3,
                Nombre = "Proyecto1"
            };
            List<Proyecto> lista = new List<Proyecto>();
            for (int i = 0; i < 2; i++)
                proyecto.Incidentes.Add(new Incidente());
            lista.Add(proyecto);
            IQueryable<Proyecto> queryableP = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true)).Returns(queryableP);
            repoGestores.Setup(c => c.Save());
            repoGestores.Setup(c => c.RepositorioIncidente.Alta(It.IsAny<Incidente>()));
            repoGestores.Setup(
                c => c.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Incidente>()))
                .Returns(proyecto.Incidentes);


            gestorProyecto.ImportarBugs(rutaFuenteTXT);

            int incidentes = gestorIncidente.ListaDeIncidentesDeLosProyectosALosQuePertenece(1, "proyecto", new Incidente()).Count();

            Assert.AreEqual(4, incidentes);
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true));
            repoGestores.Verify(c => c.Save());
            repoGestores.Verify(c => c.RepositorioIncidente.Alta(It.IsAny<Incidente>()));
            repoGestores.Verify(
                c => c.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Incidente>()));
        }

        [Test]
        public void no_se_pueden_cargar_incidentes_a_un_proyecto_si_no_existe_archivo_texto()
        {
            string rutaFuenteTXT = AppDomain.CurrentDomain.BaseDirectory + "\\Fuentes\\NoExiste.txt";
            Assert.Throws<ExcepcionElementoNoExiste>(() => gestorProyecto.ImportarBugs(rutaFuenteTXT));
        }

        [Test]
        public void se_puede_ver_los_proyectos_a_los_cuales_pertenece()
        {
            List<Proyecto> lista = new List<Proyecto>();
            lista.Add(new Proyecto());
            IQueryable<Proyecto> queryableP = lista.AsQueryable();

            List<Usuario> listaU = new List<Usuario>();
            listaU.Add(new Usuario());
            IQueryable<Usuario> queryableU = listaU.AsQueryable();

            repoGestores.Setup(
                c => c.RepositorioUsuario
                .ListaDeProyectosALosQuePertenece(It.IsAny<int>()))
                .Returns(queryableP);

            IQueryable<Proyecto> proyectos = gestorProyecto.ListaDeProyectosALosQuePertenece(usuarioCompleto.Id);

            Assert.AreEqual(1, proyectos.Count());
            repoGestores.Verify(
                c => c.RepositorioUsuario
                .ListaDeProyectosALosQuePertenece(It.IsAny<int>()));
        }
    }
}
