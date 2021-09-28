using NUnit.Framework;
using Incidentes.Datos;
using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using System.Linq;

namespace Incidentes.DatosTest
{
    public class RepositorioProyectoTest : RepositorioBaseTest
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
            Assert.IsInstanceOf(typeof(IRepositorioProyecto), _repoGestores.RepositorioProyecto);
        }

        [Test]
        public void se_puede_buscar_un_proyecto_por_nombre()
        {
            Proyecto pro = new Proyecto()
            {
                Nombre = "Proyecto del test"
            };
            DBContexto.Add(pro);
            DBContexto.SaveChanges();

            Proyecto buscado = _repoGestores.RepositorioProyecto.ObtenerPorCondicion(p => p.Nombre == pro.Nombre, false).FirstOrDefault();
            Assert.IsNotNull(buscado);
            Assert.AreEqual(pro.Nombre, buscado.Nombre);
        }

        [Test]
        public void no_se_puede_encontrar_un_proyecto_que_no_existe()
        {
            Proyecto buscado = _repoGestores.RepositorioProyecto.ObtenerPorCondicion(p => p.Nombre == "Nombre que no existe", false).FirstOrDefault();
            Assert.IsNull(buscado);
        }

        [Test]
        public void se_pueden_obtener_todos()
        {
            var buscados = _repoGestores.RepositorioProyecto.ObtenerTodos(false);
            Assert.AreEqual(2, buscados.Count());
        }

        [Test]
        public void existe_debe_devolver_true_si_existe()
        {
            Proyecto p = new Proyecto()
            {
                Nombre = "Proyecto del test"
            };
            DBContexto.Add(p);
            DBContexto.SaveChanges();

            bool existe = _repoGestores.RepositorioProyecto.Existe(pro => pro.Nombre == p.Nombre);
            Assert.IsTrue(existe);
        }

        [Test]
        public void existe_debe_devolver_false_si_no_existe()
        {
            bool existe = _repoGestores.RepositorioProyecto.Existe(p => p.Nombre == "nombre que no existe");
            Assert.IsFalse(existe);
        }

        [Test]
        public void se_puede_agregar_desarrolladores_a_un_proyecto()
        {
            Proyecto buscado = _repoGestores.RepositorioProyecto.ObtenerProyectoPorIdCompleto(1);
            Assert.AreEqual(2, buscado.Desarrolladores.Count());
        }

        [Test]
        public void se_puede_agregar_testers_a_un_proyecto()
        {
            Proyecto buscado = _repoGestores.RepositorioProyecto.ObtenerProyectoPorIdCompleto(1);
            Assert.AreEqual(2, buscado.Testers.Count());
        }

        [Test]
        public void se_puede_agregar_incidentes_a_un_proyecto()
        {
            Proyecto buscado = _repoGestores.RepositorioProyecto.ObtenerProyectoPorIdCompleto(1);
            Assert.AreEqual(1, buscado.Incidentes.Count());
        }

        [Test]
        public void se_puede_ver_la_cantidad_de_incidentes_por_proyecto_proyecto()
        {
            IQueryable<Proyecto> buscados = _repoGestores.RepositorioProyecto.ObtenerProyectosConIncidentes();
            Assert.AreEqual(1, buscados.First().Incidentes.Count());
        }

        [Test]
        public void se_puede_verificar_si_el_usuario_pertenece_al_proyecto()
        {
            bool pertenece = _repoGestores.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(1, 1);
            Assert.IsTrue(pertenece);
            Usuario d3 = new Desarrollador()
            {
                Nombre = "Martin",
                Apellido = "Cosa",
                Contrasenia = "Casa#Blanca",
                Email = "martind3@gmail.com",
                NombreUsuario = "martincosad3",
                Token = ""
            };

            DBContexto.Add(d3);
            DBContexto.SaveChanges();

            Usuario u3 = _repoGestores.RepositorioUsuario.ObtenerPorCondicion(u => u.NombreUsuario == d3.NombreUsuario, false).FirstOrDefault();
            pertenece = _repoGestores.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(u3.Id, 1);
            Assert.IsFalse(pertenece);
        }

        [Test]
        public void se_puede_verificar_si_el_incidente_pertenece_al_proyecto()
        {
            bool pertenece = _repoGestores.RepositorioProyecto.VerificarIncidentePerteneceAlProyecto(1, 1);
            Assert.IsTrue(pertenece);
            Incidente i3 = new Incidente()
            {
                Nombre = "Incidente 3"
            };

            DBContexto.Add(i3);
            DBContexto.SaveChanges();

            Incidente creado = _repoGestores.RepositorioIncidente.ObtenerPorCondicion(i => i.Nombre == i3.Nombre, false).FirstOrDefault();
            pertenece = _repoGestores.RepositorioProyecto.VerificarIncidentePerteneceAlProyecto(creado.Id, 1);
            Assert.IsFalse(pertenece);
        }

        [Test]
        public void se_pueden_agregar_desarrolladores_a_un_proyecto()
        {
            Proyecto buscado = _repoGestores.RepositorioProyecto.ObtenerProyectoPorIdCompleto(1);
            Desarrollador des = (Desarrollador)_repoGestores.RepositorioUsuario.ObtenerPorCondicion(u => u.Id == buscado.Desarrolladores.FirstOrDefault().Id, false).FirstOrDefault();
            buscado.Desarrolladores.Add(new Desarrollador());
            _repoGestores.RepositorioProyecto.Modificar(buscado);
            Proyecto nuevaCondicion = _repoGestores.RepositorioProyecto.ObtenerProyectoPorIdCompleto(1);
            Assert.AreEqual(3, nuevaCondicion.Desarrolladores.Count());
        }
    }
}