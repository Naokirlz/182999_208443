using NUnit.Framework;
using Incidentes.Datos;
using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using System.Linq;
using System.Collections.Generic;

namespace Incidentes.DatosTest
{
    public class RepositorioUsuarioTest : RepositorioBaseTest
    {
        IRepositorioGestores _repoGestores;
        [SetUp]
        public void Setup()
        {
            SetearBaseDeDatos();
            _repoGestores = new RepositorioGestores(DBContexto);
        }

        [TearDown]
        public void TearDown()
        {
            LimpiarBD();
            _repoGestores = null;
        }

        [Test]
        public void correcto_tipo_de_repositorio()
        {
            Assert.IsInstanceOf(typeof(IRepositorioUsuario), _repoGestores.RepositorioUsuario);
        }

        [Test]
        public void se_puede_buscar_un_usuario_por_nombreusuario()
        {
            Usuario a2 = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                Email = "martinf@gmail.com",
                NombreUsuario = "martincosaf",
                Token = ""
            };
            DBContexto.Add(a2);
            DBContexto.SaveChanges();
          
            Usuario buscado = _repoGestores.RepositorioUsuario.ObtenerPorCondicion(u => u.NombreUsuario == a2.NombreUsuario, false).FirstOrDefault();
            Assert.IsNotNull(buscado);
        }

        [Test]
        public void no_se_puede_encontrar_un_usuario_que_no_existe()
        {
            Usuario buscado = _repoGestores.RepositorioUsuario.ObtenerPorCondicion(u => u.NombreUsuario == "Nombre que no existe", false).FirstOrDefault();
            Assert.IsNull(buscado);
        }

        [Test]
        public void se_pueden_obtener_todos()
        {
            var buscados = _repoGestores.RepositorioUsuario.ObtenerTodos(false);
            Assert.AreEqual(4, buscados.Count());
        }

        [Test]
        public void existe_debe_devolver_true_si_existe()
        {
            Usuario d3 = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                Email = "martinf@gmail.com",
                NombreUsuario = "martincosaf",
                Token = ""
            };
            DBContexto.Add(d3);
            DBContexto.SaveChanges();

            bool existe = _repoGestores.RepositorioUsuario.Existe(u => u.NombreUsuario == d3.NombreUsuario);
            Assert.IsTrue(existe);
        }

        [Test]
        public void existe_debe_devolver_false_si_no_existe()
        {
            bool existe = _repoGestores.RepositorioUsuario.Existe(u => u.NombreUsuario == "nombre que no existe");
            Assert.IsFalse(existe);
        }

        [Test]
        public void se_devuelven_la_cantidad_de_incidentes_resueltos_por_un_desarrollador()
        {
            int cantidadResueltos = _repoGestores.RepositorioUsuario.CantidadDeIncidentesResueltosPorUnDesarrollador(1);
            Assert.AreEqual(1, cantidadResueltos);
        }

        [Test]
        public void se_devuelve_la_lista_de_proyectos_a_la_que_pertenece_un_desarrollador()
        {
            IQueryable<Proyecto> proyectos = _repoGestores.RepositorioUsuario.ListaDeProyectosALosQuePertenece(1);
            Assert.AreEqual(1, proyectos.Count());
        }

        [Test]
        public void se_devuelve_la_lista_de_proyectos_a_la_que_pertenece_un_tester()
        {
            IQueryable<Proyecto> proyectos = _repoGestores.RepositorioUsuario.ListaDeProyectosALosQuePertenece(3);
            Assert.AreEqual(2, proyectos.Count());
        }

        [Test]
        public void se_devuelve_la_lista_de_incidentes_de_los_proyectos_a_la_que_pertenece_un_usuario()
        {
            List<Incidente> incidentes = _repoGestores.RepositorioUsuario.ListaDeIncidentesDeLosProyectosALosQuePertenece(3, "", new Incidente());
            Assert.AreEqual(2, incidentes.Count());
        }

        [Test]
        public void se_devuelve_la_lista_de_incidentes_de_los_proyectos_a_la_que_pertenece_un_usuario_filtrando_por_idIncidente()
        {
            List<Incidente> incidentes = _repoGestores.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(3, "", new Incidente()
                {
                    Id = 1
                });
            Assert.AreEqual(1, incidentes.Count());
        }

        [Test]
        public void se_devuelve_la_lista_de_incidentes_de_los_proyectos_a_la_que_pertenece_un_usuario_filtrando_por_nombreIncidente()
        {
            List<Incidente> incidentes = _repoGestores.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(3, "", new Incidente()
                {
                    Nombre = "1"
                });
            Assert.AreEqual(1, incidentes.Count());
        }

        [Test]
        public void se_devuelve_la_lista_de_incidentes_de_los_proyectos_a_la_que_pertenece_un_usuario_filtrando_por_estadoIncidente()
        {
            List<Incidente> incidentes = _repoGestores.RepositorioUsuario
                .ListaDeIncidentesDeLosProyectosALosQuePertenece(3, "", new Incidente()
                {
                    EstadoIncidente = Incidente.Estado.Resuelto
                });
            Assert.AreEqual(1, incidentes.Count());
        }
    }
}