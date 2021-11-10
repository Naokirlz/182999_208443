﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Incidentes.Dominio;
using Incidentes.DatosInterfaz;
using Incidentes.LogicaImportaciones;
using System.Linq.Expressions;
using Incidentes.Logica.Excepciones;

namespace Incidentes.Logica.Test
{
    class LogicaImportacionesTest
    {
        Mock<IRepositorioGestores> repoGestores;
        LogicaImportacion logicaImportaciones;
        GestorIncidente gestorIncidente;

        [SetUp]
        public void SetUp()
        {
            repoGestores = new Mock<IRepositorioGestores>();
            logicaImportaciones = new LogicaImportacion(repoGestores.Object);
            gestorIncidente = new GestorIncidente(repoGestores.Object);
        }

        [TearDown]
        public void TearDown()
        {
            repoGestores = null;
            gestorIncidente = null;
            logicaImportaciones = null;
        }

        [Test]
        public void se_pueden_cargar_incidentes_a_un_proyecto_con_xml()
        {
            string rutaFuenteXML = "C:\\Users\\federico\\Documents\\GitHub\\182999_208443\\Documentacion\\Accesorios-Postman-Fuentes\\FuenteXML.xml";
            string rutaBinarios = "C:\\Users\\federico\\Documents\\GitHub\\182999_208443\\Documentacion\\Accesorios-Postman-Fuentes\\DLLs\\Incidentes.ImportacionesXML";

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


            logicaImportaciones.ImportarBugs(rutaFuenteXML, rutaBinarios, 5);

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
            string rutaBinarios = "C:\\Users\\federico\\Documents\\GitHub\\182999_208443\\Documentacion\\Accesorios-Postman-Fuentes\\DLLs\\Incidentes.ImportacionesXML";
            Assert.Throws<ExcepcionElementoNoExiste>(() => logicaImportaciones.ImportarBugs(rutaFuenteXML, rutaBinarios, 5));
        }

        [Test]
        public void se_pueden_cargar_incidentes_a_un_proyecto_con_texto()
        {
            // string rutaFuenteTXT = AppDomain.CurrentDomain.BaseDirectory + "\\Fuentes\\Fuente.txt";
            string rutaFuenteTXT = "C:\\Users\\federico\\Documents\\GitHub\\182999_208443\\Documentacion\\Accesorios-Postman-Fuentes\\FuenteTXT.txt";
            string rutaBinarios = "C:\\Users\\federico\\Documents\\GitHub\\182999_208443\\Documentacion\\Accesorios-Postman-Fuentes\\DLLs\\Incidentes.ImportacionesTXT";

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


            logicaImportaciones.ImportarBugs(rutaFuenteTXT, rutaBinarios, 5);

            int incidentes = gestorIncidente.ListaDeIncidentesDeLosProyectosALosQuePertenece(1, "proyecto", new Incidente()).Count();

            Assert.AreEqual(5, incidentes);
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
            string rutaBinarios = "C:\\Users\\federico\\Documents\\GitHub\\182999_208443\\Documentacion\\Accesorios-Postman-Fuentes\\DLLs\\Incidentes.ImportacionesTXT";
            Assert.Throws<ExcepcionElementoNoExiste>(() => logicaImportaciones.ImportarBugs(rutaFuenteTXT, rutaBinarios, 5));
        }

        [Test]
        public void se_pueden_obtener_una_lista_de_plugin_de_importaciones()
        {
            List<string> lista = logicaImportaciones.ListarPlugins();

            Assert.AreEqual(2, lista.Count());
        }

        [Test]
        public void se_pueden_cargar_incidentes_a_un_proyecto_con_json()
        {
            string rutaFuenteJSON = "C:\\Users\\federico\\Documents\\GitHub\\182999_208443\\Documentacion\\Accesorios-Postman-Fuentes\\FuenteJSON.json";
            string rutaBinarios = "C:\\Users\\federico\\Documents\\GitHub\\182999_208443\\Documentacion\\Accesorios-Postman-Fuentes\\DLLs\\Incidentes.ImportacionesJSON";

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


            logicaImportaciones.ImportarBugs(rutaFuenteJSON, rutaBinarios, 5);

            int incidentes = gestorIncidente.ListaDeIncidentesDeLosProyectosALosQuePertenece(1, "proyecto", new Incidente()).Count();

            Assert.AreEqual(4, incidentes);
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true));
            repoGestores.Verify(c => c.Save());
            repoGestores.Verify(c => c.RepositorioIncidente.Alta(It.IsAny<Incidente>()));
            repoGestores.Verify(
                c => c.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Incidente>()));
        }
    }
}
