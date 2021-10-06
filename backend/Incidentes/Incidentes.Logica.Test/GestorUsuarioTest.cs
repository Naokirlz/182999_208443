using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.Excepciones;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Incidentes.Logica.Test
{
    public class GestorUsuarioTest
    {
        private Usuario usuarioCompleto;
        Mock<IRepositorioGestores> repoGestores;
        GestorUsuario gestor;

        [SetUp]
        public void Setup()
        {
            this.usuarioCompleto = new Usuario() { 
                Nombre = "Martin",
                Apellido = "Cosas",
                Contrasenia = "Casa#Blancaaaaaaaaa",
                Email = "martin@gmail.com",
                Id = 1,
                NombreUsuario = "martincosassssss",
                Token = ""
            };

            repoGestores = new Mock<IRepositorioGestores>();
            gestor = new GestorUsuario(repoGestores.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this.usuarioCompleto = null;
            repoGestores = null;
            gestor = null;
        }

        [Test]
        public void se_puede_guardar_administrador()
        {
            Usuario administrador = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosas",
                Contrasenia = "Casa#Blancaaaaaaaaa",
                Email = "martin@gmail.com",
                Id = 1,
                NombreUsuario = "martincosassssss",
                Token = ""
            };    

            repoGestores.Setup(c => c.RepositorioUsuario.Alta(administrador));

            Usuario admin = gestor.Alta(administrador);

            Assert.AreEqual(administrador.Nombre, admin.Nombre);
            repoGestores.Verify(c => c.RepositorioUsuario.Alta(administrador));
        }

        [Test]
        public void se_pueden_obtener_los_desarrolladores()
        {
            Usuario d1 = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosas",
                Contrasenia = "Casa#BlanBlancaaaaaaaaaca",
                Email = "martin@gmail.com",
                RolUsuario = Usuario.Rol.Desarrollador,
                Id = 1,
                NombreUsuario = "martincosa",
                Token = ""
            };

            List<Usuario> lista = new List<Usuario>();
            lista.Add(d1);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerTodos(false)).Returns(queryableUsuarios);

            List<Usuario> listaD = gestor.Obtener(Usuario.Rol.Desarrollador);

            Assert.AreEqual(1, listaD.Count());
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerTodos(false));
        }

        [Test]
        public void se_puede_obtener_un_usuario_por_id()
        {
            Usuario d1 = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosas",
                Contrasenia = "Casa#BlanBlancaaaaaaaaaca",
                Email = "martin@gmail.com",
                Id = 1,
                RolUsuario = Usuario.Rol.Desarrollador,
                NombreUsuario = "martincosa",
                Token = ""
            };

            List<Usuario> lista = new List<Usuario>();
            lista.Add(d1);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false)).Returns(queryableUsuarios);
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);

            Usuario des = gestor.Obtener(3);

            Assert.AreEqual(d1.Nombre, des.Nombre);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_obtener_un_usuario_que_no_existe()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);

            Assert.Throws<ExcepcionElementoNoExiste>(() => gestor.Obtener(3));

            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void se_puede_guardar_desarrollador()
        {
            Usuario desarrollador1 = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosas",
                Contrasenia = "Casa#Blancaaaaaaaaa",
                Email = "martin@gmail.com",
                Id = 1,
                NombreUsuario = "martincosa",
                Token = ""
            };

            repoGestores.Setup(c => c.RepositorioUsuario.Alta(desarrollador1));

            Usuario desarrollador = gestor.Alta(desarrollador1);

            Assert.AreEqual(desarrollador1.Nombre, desarrollador.Nombre);
            repoGestores.Verify(c => c.RepositorioUsuario.Alta(desarrollador1));
        }

        [Test]
        public void se_puede_guardar_tester()
        {
            Usuario tester1 = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosas",
                Contrasenia = "Casa#Blancaaaaaaaaa",
                Email = "martin@gmail.com",
                Id = 1,
                NombreUsuario = "martincosa",
                Token = ""
            };

            repoGestores.Setup(c => c.RepositorioUsuario.Alta(tester1));

            Usuario desarrollador = gestor.Alta(tester1);

            Assert.AreEqual(tester1.Nombre, desarrollador.Nombre);
            repoGestores.Verify(c => c.RepositorioUsuario.Alta(tester1));
        }

        [Test]
        public void se_puede_obtener_un_tester_por_token()
        {
            Usuario t1 = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosas",
                Contrasenia = "Casa#BlanBlancaaaaaaaaaca",
                Email = "martin@gmail.com",
                Id = 1,
                NombreUsuario = "martincosa",
                Token = ""
            };

            List<Usuario> lista = new List<Usuario>();
            lista.Add(t1);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false)).Returns(queryableUsuarios);
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);

            Usuario usuario = gestor.ObtenerPorToken("s");

            Assert.AreEqual(t1.Nombre, usuario.Nombre);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_obtener_un_usuario_con_token_que_no_existe()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);

            Assert.Throws<ExcepcionElementoNoExiste>(() => gestor.ObtenerPorToken("s"));

            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void se_pueden_obtener_los_tester()
        {
            Usuario t1 = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosas",
                Contrasenia = "Casa#BlanBlancaaaaaaaaaca",
                Email = "martin@gmail.com",
                RolUsuario = Usuario.Rol.Tester,
                Id = 1,
                NombreUsuario = "martincosa",
                Token = ""
            };

            List<Usuario> lista = new List<Usuario>();
            lista.Add(t1);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerTodos(false)).Returns(queryableUsuarios);

            List<Usuario> listaT = gestor.Obtener(Usuario.Rol.Tester);

            Assert.AreEqual(1, listaT.Count());
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerTodos(false));
        }

        [Test]
        public void se_pueden_obtener_los_usuarios()
        {
            Usuario t1 = new Usuario()
            {
                Nombre = "Martin",
                Apellido = "Cosas",
                Contrasenia = "Casa#BlanBlancaaaaaaaaaca",
                Email = "martin@gmail.com",
                RolUsuario = Usuario.Rol.Tester,
                Id = 1,
                NombreUsuario = "martincosa",
                Token = ""
            };

            List<Usuario> lista = new List<Usuario>();
            lista.Add(t1);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerTodos(false)).Returns(queryableUsuarios);

            List<Usuario> listaU = gestor.Obtener();

            Assert.AreEqual(1, listaU.Count());
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerTodos(false));
        }

        [Test]
        public void un_usuario_se_puede_loguear()
        {
            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);

            string loginCorrecto = gestor.Login(this.usuarioCompleto.NombreUsuario, queryableUsuarios.FirstOrDefault().Contrasenia);

            Assert.IsNotNull(loginCorrecto);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
        }

        [Test]
        public void un_usuario_no_se_puede_loguear_con_contrasenia_incorrecta()
        {
            List<Usuario> lista = new List<Usuario>();
            usuarioCompleto.Token = "";
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);

            string loginCorrecto = gestor.Login(this.usuarioCompleto.NombreUsuario, "password incorrecto");

            Assert.AreEqual("", usuarioCompleto.Token);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
        }

        [Test]
        public void un_usuario_al_loguearse_recibe_un_token()
        {
            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            List<Usuario> listaVacia = new List<Usuario>();
            IQueryable<Usuario> queryableUsuariosVacia = listaVacia.AsQueryable();

            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            repoGestores.Setup(c => c.RepositorioUsuario.Modificar(It.IsAny<Usuario>()));

            gestor.Login(this.usuarioCompleto.NombreUsuario, queryableUsuarios.FirstOrDefault().Contrasenia);

            Assert.IsNotEmpty(usuarioCompleto.Token);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
            repoGestores.Verify(c => c.RepositorioUsuario.Modificar(It.IsAny<Usuario>()));
        }

        [Test]
        public void un_usuario_logueado_se_puede_desloguear()
        {
            usuarioCompleto.Token = "asdasdasdasdasdasdasd";

            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);

            gestor.Logout(usuarioCompleto.Token);

            Assert.IsEmpty(usuarioCompleto.Token);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
        }

        [Test]
        public void un_administrador_puede_ver_cantidad_de_bugs_resueltos_por_un_desarrollador()
        {
            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();

            repoGestores.Setup(c => c.RepositorioUsuario.CantidadDeIncidentesResueltosPorUnDesarrollador(It.IsAny<int>())).Returns(5);

            int incidentes = gestor.CantidadDeIncidentesResueltosPorUnDesarrollador(3);

            Assert.AreEqual(5, incidentes);
            repoGestores.Verify(c => c.RepositorioUsuario.CantidadDeIncidentesResueltosPorUnDesarrollador(It.IsAny<int>()));
        }

        [Test]
        public void no_se_puede_dar_alta_un_usuario_nulo()
        {
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(null));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_nombre_repetido()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(true);

            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(new Usuario()));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_nombre_corto()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioCompleto.Nombre = "ss s";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(usuarioCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_nombre_largo()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioCompleto.Nombre = "01234567890123456789012345";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(usuarioCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_apellido_corto()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioCompleto.Apellido = "ss s";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(usuarioCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_apellido_largo()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioCompleto.Apellido = "01234567890123456789012345";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(usuarioCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_nombreUsuario_corto()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioCompleto.NombreUsuario = "ss s";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(usuarioCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_nombreUsuario_largo()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioCompleto.NombreUsuario = "01234567890123456789012345";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(usuarioCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_password_invalido()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioCompleto.Contrasenia = "    ssa   a";
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(usuarioCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_password_corto()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioCompleto.Contrasenia = "aa";
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(usuarioCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_password_largo()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioCompleto.Contrasenia = "0123456789012345678901234567";
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(usuarioCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_email_invalido()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioCompleto.Email = "ssddassssas";
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(usuarioCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }
    }
}