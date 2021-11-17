using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.DTOs;
using Incidentes.Excepciones;
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
            Proyecto p = new Proyecto() { Nombre = "Proyecto" };
            List<Proyecto> lp = new List<Proyecto>();
            lp.Add(p);
            IQueryable<Proyecto> iqp = lp.AsQueryable();
            repoGestores.Setup(c => c.RepositorioIncidente.Alta(incidente));
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false)).Returns(iqp);
            IncidenteDTO inc01 = gestorIncidente.Alta(new IncidenteDTO(incidente));

            Assert.AreEqual(incidente.Nombre, inc01.Nombre);
            repoGestores.Verify(c => c.RepositorioIncidente.Alta(It.IsAny<Incidente>()));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false));
        }

        [Test]
        public void un_usuario_logueado_puede_ver_incidentes()
        {
            Proyecto p = new Proyecto() { Nombre = "Proyecto" };
            List<Proyecto> lp = new List<Proyecto>();
            lp.Add(p);
            IQueryable<Proyecto> iqp = lp.AsQueryable();
            Incidente inc1 = new Incidente();
            Incidente inc2 = new Incidente();
            listaI.Add(inc1);
            listaI.Add(inc2);
            listaU.Add(usuarioCompleto);
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false)).Returns(iqp);

            List<IncidenteDTO> lista = (List<IncidenteDTO>)gestorIncidente.ObtenerTodos();

            Assert.AreEqual(2, lista.Count());
            repoGestores.Verify(c => c.RepositorioIncidente.ObtenerTodos(false));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false));
        }

        [Test]
        public void alta_devuelve_una_instancia_de_incidente()
        {
            Proyecto p = new Proyecto() { Nombre = "Proyecto" };
            List<Proyecto> lp = new List<Proyecto>();
            lp.Add(p);
            IQueryable<Proyecto> iqp = lp.AsQueryable();
            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(false);
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false)).Returns(iqp);

            IncidenteDTO incidente02 = gestorIncidente.Alta(new IncidenteDTO(incidente));
            Assert.IsNotNull(incidente02);
            repoGestores.Verify(c => c.RepositorioIncidente.Alta(It.IsAny<Incidente>()));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false));
        }

        [Test]
        public void se_puede_eliminar_un_incidente()
        {
            Proyecto p = new Proyecto() { Nombre = "Proyecto" };
            List<Proyecto> lp = new List<Proyecto>();
            lp.Add(p);
            IQueryable<Proyecto> iqp = lp.AsQueryable();
            Incidente incidenteD = new Incidente()
            {
                Id = 2,
                Nombre = "Incidente",
                ProyectoId = 3
            };
            List<Incidente> lista = new List<Incidente>();
            lista.Add(incidenteD);
            IQueryable<Incidente> queryableI = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), false)).Returns(queryableI);
            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false)).Returns(iqp);

            gestorIncidente.Baja(5);
            List<IncidenteDTO> incidentes = gestorIncidente.ObtenerTodos().ToList();
            Assert.AreEqual(0, incidentes.Count());
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
            repoGestores.Verify(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioIncidente.ObtenerTodos(false));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false));
        }

        [Test]
        public void no_se_puede_modificar_un_incidente_que_no_existe()
        {
            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(false);

            Assert.Throws<ExcepcionElementoNoExiste>(() => gestorIncidente.Modificar(20, new IncidenteDTO()));

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
            Assert.Throws<ExcepcionLargoTexto>(() => gestorIncidente.Alta(new IncidenteDTO()
            {
                Nombre = "ae"
            }));
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
        }

        [Test]
        public void no_se_puede_crear_un_incidente_con_nombre_largo()
        {
            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(false);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            string textoLargo = new string(Enumerable.Repeat(chars, 51)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            Assert.Throws<ExcepcionLargoTexto>(() => gestorIncidente.Alta(new IncidenteDTO()
            {
                Nombre = textoLargo
            }));
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
        }

        [Test]
        public void se_puede_ver_la_lista_de_bugs_de_los_proyectos_que_pertenece()
        {
            Proyecto p = new Proyecto() { Nombre = "Proyecto" };
            List<Proyecto> lp = new List<Proyecto>();
            lp.Add(p);
            IQueryable<Proyecto> iqp = lp.AsQueryable();
            List<Incidente> lista = new List<Incidente>();
            lista.Add(new Incidente());

            List<Usuario> listaU = new List<Usuario>();
            listaU.Add(new Usuario());
            IQueryable<Usuario> queryableU = listaU.AsQueryable();

            repoGestores.Setup(
                c => c.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Incidente>()))
                .Returns(lista);
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false)).Returns(iqp);

            List<IncidenteDTO> incidentes = gestorIncidente.ListaDeIncidentesDeLosProyectosALosQuePertenece(usuarioCompleto.Id, "", new IncidenteDTO());

            Assert.AreEqual(1, lista.Count());
            repoGestores.Verify(
                c => c.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<Incidente>()));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false));
        }

        [Test]
        public void se_puede_ver_un_incidente_de_un_usuario()
        {
            Proyecto p = new Proyecto() { Nombre = "Proyecto" };
            List<Proyecto> lp = new List<Proyecto>();
            lp.Add(p);
            IQueryable<Proyecto> iqp = lp.AsQueryable();
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
            repoGestores.Setup(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), false)).Returns(queryableI);
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false)).Returns(iqp);

            IncidenteDTO encontrado = gestorIncidente.ObtenerParaUsuario(1, 2);

            Assert.AreEqual(incidenteD.Nombre, encontrado.Nombre);
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
            repoGestores.Verify(c => c.RepositorioProyecto.VerificarIncidentePerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
            repoGestores.Verify(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false));
        }

        [Test]
        public void no_se_puede_ver_si_incidente_no_pertenece_a_proyecto()
        {
            Proyecto p = new Proyecto() { Nombre = "Proyecto" };
            List<Proyecto> lp = new List<Proyecto>();
            lp.Add(p);
            IQueryable<Proyecto> iqp = lp.AsQueryable();
            Incidente incidenteD = new Incidente()
            {
                Id = 2,
                Nombre = "Incidente"
            };
            List<Incidente> lista = new List<Incidente>();
            lista.Add(incidenteD);
            IQueryable<Incidente> queryableI = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), false)).Returns(queryableI);
            repoGestores.Setup(c => c.RepositorioProyecto.VerificarIncidentePerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false)).Returns(iqp);
            Assert.Throws<ExcepcionAccesoNoAutorizado>(() => gestorIncidente.ObtenerParaUsuario(1, 2));
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
            repoGestores.Verify(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioProyecto.VerificarIncidentePerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false));
        }

        [Test]
        public void no_se_puede_dar_alta_un_incidente_nulo()
        {
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestorIncidente.Alta(null));
        }

        [Test]
        public void no_se_puede_dar_alta_un_incidente_que_ya_existe()
        {
            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(true);
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestorIncidente.Alta(new IncidenteDTO()));
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
        }

        [Test]
        public void no_se_puede_modificar_un_incidente_nulo()
        {
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestorIncidente.Modificar(1, null));
        }

        [Test]
        public void no_se_puede_modificar_un_incidente_que_ya_existe()
        {
            Proyecto p = new Proyecto() { Nombre = "Proyecto" };
            List<Proyecto> lp = new List<Proyecto>();
            lp.Add(p);
            IQueryable<Proyecto> iqp = lp.AsQueryable();
            Incidente incidenteD = new Incidente()
            {
                Id = 2,
                Nombre = "Incidente"
            };
            IncidenteDTO incidenteA = new IncidenteDTO()
            {
                Id = 2,
                Nombre = "Incidente2"
            };
            List<Incidente> lista = new List<Incidente>();
            lista.Add(incidenteD);
            IQueryable<Incidente> queryableI = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), false)).Returns(queryableI);
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false)).Returns(iqp);

            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestorIncidente.Modificar(1, incidenteA));
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
            repoGestores.Verify(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false));
        }

        [Test]
        public void se_puede_modificar_un_incidente()
        {
            Usuario u = new Usuario()
            {
                Id = 7,
                Nombre = "aa",
                Apellido = "bb"
            };
            List<Usuario> lu = new List<Usuario>();
            lu.Add(u);
            IQueryable<Usuario> iqu = lu.AsQueryable();
            Proyecto p = new Proyecto() { Nombre = "Proyecto" };
            List<Proyecto> lp = new List<Proyecto>();
            lp.Add(p);
            IQueryable<Proyecto> iqp = lp.AsQueryable();
            Incidente incidenteD = new Incidente()
            {
                Id = 2,
                Nombre = "Incidente"
            };
            IncidenteDTO incidenteA = new IncidenteDTO()
            {
                Id = 2,
                Nombre = "Incidente2",
                DesarrolladorId = 5,
                Descripcion = "nueva Desc",
                EstadoIncidente = IncidenteDTO.Estado.Resuelto,
                ProyectoId = 5,
                Version = "2.0"
            };
            List<Incidente> lista = new List<Incidente>();
            lista.Add(incidenteD);
            IQueryable<Incidente> queryableI = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioIncidente.Existe(c => c.Nombre == incidenteA.Nombre)).Returns(false);
            repoGestores.Setup(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), false)).Returns(queryableI);
            repoGestores.Setup(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false)).Returns(iqp);
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false)).Returns(iqu);
            
            IncidenteDTO modificado = gestorIncidente.Modificar(1, incidenteA);

            Assert.AreEqual(modificado.Nombre, incidenteA.Nombre);

            repoGestores.Verify(c => c.RepositorioIncidente.Existe(It.IsAny<Expression<Func<Incidente, bool>>>()));
            repoGestores.Verify(c => c.RepositorioIncidente.Existe(c => c.Nombre == incidenteA.Nombre));
            repoGestores.Verify(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
            repoGestores.Verify(c => c.RepositorioIncidente.ObtenerPorCondicion(It.IsAny<Expression<Func<Incidente, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioProyecto.ObtenerPorCondicion(It.IsAny<Expression<Func<Proyecto, bool>>>(), false));
        }

        [Test]
        public void no_se_puede_resolver_un_incidente_si_el_usuario_no_pertenece_al__proyecto()
        {
            repoGestores.Setup(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            repoGestores.Setup(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(2, It.IsAny<int>())).Returns(false);
            Assert.Throws<ExcepcionAccesoNoAutorizado>(() => gestorIncidente.Modificar(1, new IncidenteDTO() { DesarrolladorId = 2}));
            repoGestores.Verify(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(It.IsAny<int>(), It.IsAny<int>()));
            repoGestores.Verify(c => c.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(2, It.IsAny<int>()));
        }
    }
}
