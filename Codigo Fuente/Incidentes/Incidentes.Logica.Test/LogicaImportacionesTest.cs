using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using Incidentes.Dominio;
using Incidentes.DatosInterfaz;
using System.Linq.Expressions;
using System.IO;
using Incidentes.DTOs;
using Incidentes.Excepciones;

namespace Incidentes.Logica.Test
{
    class LogicaImportacionesTest
    {
        Mock<IRepositorioGestores> repoGestores;
        GestorImportacion logicaImportaciones;
        GestorIncidente gestorIncidente;
        private string directorio_base;

        [SetUp]
        public void SetUp()
        {
            repoGestores = new Mock<IRepositorioGestores>();
            logicaImportaciones = new GestorImportacion(repoGestores.Object);
            gestorIncidente = new GestorIncidente(repoGestores.Object);
            directorio_base = Directory.GetCurrentDirectory();
            for (int i = 0; i < 6; i++)
            {
                directorio_base = System.IO.Directory.GetParent(directorio_base).FullName;
            }
        }

        [TearDown]
        public void TearDown()
        {
            repoGestores = null;
            gestorIncidente = null;
            logicaImportaciones = null;
            directorio_base = "";
        }

        [Test]
        public void se_pueden_cargar_incidentes_a_un_proyecto_con_xml()
        {
            Proyecto p = new Proyecto() { Nombre = "Proyecto" };
            List<Proyecto> lp = new List<Proyecto>();
            lp.Add(p);
            IQueryable<Proyecto> iqp = lp.AsQueryable();
            string rutaFuenteXML = directorio_base + "\\Documentacion\\Accesorios-Postman-Fuentes\\FuenteXML.xml";
            string rutaBinarios = directorio_base + "\\Documentacion\\Accesorios-Postman-Fuentes\\DLLs\\Incidentes.ImportacionXML";

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
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false)).Returns(iqp);


            logicaImportaciones.ImportarBugs(rutaFuenteXML, rutaBinarios, 5);

            int incidentes = gestorIncidente.ListaDeIncidentesDeLosProyectosALosQuePertenece(1, "proyecto", new IncidenteDTO()).Count();

            Assert.AreEqual(4, incidentes);
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true));
            repoGestores.Verify(c => c.Save());
            repoGestores.Verify(c => c.RepositorioIncidente.Alta(It.IsAny<Incidente>()));
            repoGestores.Verify(
                c => c.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Incidente>()));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false));
        }

        [Test]
        public void no_se_pueden_cargar_incidentes_a_un_proyecto_si_no_existe_archivo_xml()
        {
            string rutaFuenteXML = AppDomain.CurrentDomain.BaseDirectory + "\\Fuentes\\NoExiste.xml";
            string rutaBinarios = directorio_base + "\\Documentacion\\Accesorios-Postman-Fuentes\\DLLs\\Incidentes.ImportacionesXML";
            Assert.Throws<ExcepcionElementoNoExiste>(() => logicaImportaciones.ImportarBugs(rutaFuenteXML, rutaBinarios, 5));
        }

        [Test]
        public void se_pueden_cargar_incidentes_a_un_proyecto_con_texto()
        {
            Proyecto p = new Proyecto() { Nombre = "Proyecto" };
            List<Proyecto> lp = new List<Proyecto>();
            lp.Add(p);
            IQueryable<Proyecto> iqp = lp.AsQueryable();
            string rutaFuenteTXT = directorio_base + "\\Documentacion\\Accesorios-Postman-Fuentes\\FuenteTXT.txt";
            string rutaBinarios = directorio_base + "\\Documentacion\\Accesorios-Postman-Fuentes\\DLLs\\Incidentes.ImportacionesTXT";

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
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false)).Returns(iqp);


            logicaImportaciones.ImportarBugs(rutaFuenteTXT, rutaBinarios, 5);

            int incidentes = gestorIncidente.ListaDeIncidentesDeLosProyectosALosQuePertenece(1, "proyecto", new IncidenteDTO()).Count();

            Assert.AreEqual(5, incidentes);
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true));
            repoGestores.Verify(c => c.Save());
            repoGestores.Verify(c => c.RepositorioIncidente.Alta(It.IsAny<Incidente>()));
            repoGestores.Verify(
                c => c.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Incidente>()));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false));
        }

        [Test]
        public void no_se_pueden_cargar_incidentes_a_un_proyecto_si_no_existe_archivo_texto()
        {
            string rutaFuenteTXT = AppDomain.CurrentDomain.BaseDirectory + "\\Fuentes\\NoExiste.txt";
            string rutaBinarios = directorio_base + "\\Documentacion\\Accesorios-Postman-Fuentes\\DLLs\\Incidentes.ImportacionesTXT";
            Assert.Throws<ExcepcionElementoNoExiste>(() => logicaImportaciones.ImportarBugs(rutaFuenteTXT, rutaBinarios, 5));
        }

        [Test]
        public void se_pueden_obtener_una_lista_de_plugin_de_importaciones()
        {
            List<string> lista = logicaImportaciones.ListarPlugins();

            Assert.IsTrue(lista.Count()> 0);
        }

        [Test]
        public void se_pueden_cargar_incidentes_a_un_proyecto_con_json()
        {
            Proyecto p = new Proyecto() { Nombre = "Proyecto" };
            List<Proyecto> lp = new List<Proyecto>();
            lp.Add(p);
            IQueryable<Proyecto> iqp = lp.AsQueryable();
            string rutaFuenteJSON = directorio_base + "\\Documentacion\\Accesorios-Postman-Fuentes\\FuenteJSON.json";
            string rutaBinarios = directorio_base + "\\Documentacion\\Accesorios-Postman-Fuentes\\DLLs\\Incidentes.ImportacionesJSON";

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
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false)).Returns(iqp);


            logicaImportaciones.ImportarBugs(rutaFuenteJSON, rutaBinarios, 5);

            int incidentes = gestorIncidente.ListaDeIncidentesDeLosProyectosALosQuePertenece(1, "proyecto", new IncidenteDTO()).Count();

            Assert.AreEqual(4, incidentes);
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), true));
            repoGestores.Verify(c => c.Save());
            repoGestores.Verify(c => c.RepositorioIncidente.Alta(It.IsAny<Incidente>()));
            repoGestores.Verify(
                c => c.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Incidente>()));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false));
        }
    }
}
