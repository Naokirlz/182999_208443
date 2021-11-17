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
    public class GestorUsuarioTest
    {
        private UsuarioDTO usuarioDTOCompleto;
        private Usuario usuarioCompleto;
        Mock<IRepositorioGestores> repoGestores;
        GestorUsuario gestor;

        [SetUp]
        public void Setup()
        {
            this.usuarioDTOCompleto = new UsuarioDTO() { 
                Nombre = "Martin",
                Apellido = "Cosas",
                Contrasenia = "Casa#Blancaaaaaaaaa",
                Email = "martin@gmail.com",
                Id = 1,
                NombreUsuario = "martincosassssss",
                Token = ""
            };

            this.usuarioCompleto = usuarioDTOCompleto.convertirDTO_Dominio();

            repoGestores = new Mock<IRepositorioGestores>();
            gestor = new GestorUsuario(repoGestores.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this.usuarioDTOCompleto = null;
            this.usuarioCompleto = null;
            repoGestores = null;
            gestor = null;
        }

        [Test]
        public void se_puede_guardar_administrador()
        {
            UsuarioDTO administrador = new UsuarioDTO()
            {
                Nombre = "Martin",
                Apellido = "Cosas",
                Contrasenia = "Casa#Blancaaaaaaaaa",
                Email = "martin@gmail.com",
                Id = 1,
                NombreUsuario = "martincosassssss",
                Token = ""
            };    

            repoGestores.Setup(c => c.RepositorioUsuario.Alta(It.IsAny<Usuario>()));

            UsuarioDTO admin = gestor.Alta(administrador);

            Assert.AreEqual(administrador.Nombre, admin.Nombre);
            repoGestores.Verify(c => c.RepositorioUsuario.Alta(It.IsAny<Usuario>()));
        }

        //[Test]
        //public void se_pueden_obtener_los_desarrolladores()
        //{
        //    UsuarioDTO d1 = new UsuarioDTO()
        //    {
        //        Nombre = "Martin",
        //        Apellido = "Cosas",
        //        Contrasenia = "Casa#BlanBlancaaaaaaaaaca",
        //        Email = "martin@gmail.com",
        //        RolUsuario = UsuarioDTO.Rol.Desarrollador,
        //        Id = 1,
        //        NombreUsuario = "martincosa",
        //        Token = ""
        //    };

        //    List<Usuario> lista = new List<Usuario>();
        //    lista.Add(d1.convertirDTO_Dominio());
        //    IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();

        //    repoGestores.Setup(c => c.RepositorioUsuario.ObtenerTodos(false)).Returns(queryableUsuarios);

        //    List<Usuario> listaD = gestor.Obtener(Usuario.Rol.Desarrollador);

        //    Assert.AreEqual(1, listaD.Count());
        //    repoGestores.Verify(c => c.RepositorioUsuario.ObtenerTodos(false));
        //}

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

            UsuarioDTO des = gestor.Obtener(3);

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
        public void no_se_puede_loguear_un_usuario_que_no_existe()
        {
            List<Usuario> lista = new List<Usuario>();
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), It.IsAny <bool>())).Returns(queryableUsuarios);

            Assert.Throws<ExcepcionElementoNoExiste>(() => gestor.Login("ssss","ssss"));

            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), It.IsAny<bool>()));
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

            UsuarioDTO usuario = gestor.ObtenerPorToken("s");

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

        //[Test]
        //public void se_pueden_obtener_los_tester()
        //{
        //    Usuario t1 = new Usuario()
        //    {
        //        Nombre = "Martin",
        //        Apellido = "Cosas",
        //        Contrasenia = "Casa#BlanBlancaaaaaaaaaca",
        //        Email = "martin@gmail.com",
        //        RolUsuario = Usuario.Rol.Tester,
        //        Id = 1,
        //        NombreUsuario = "martincosa",
        //        Token = ""
        //    };

        //    List<Usuario> lista = new List<Usuario>();
        //    lista.Add(t1);
        //    IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();

        //    repoGestores.Setup(c => c.RepositorioUsuario.ObtenerTodos(false)).Returns(queryableUsuarios);

        //    List<Usuario> listaT = gestor.Obtener(Usuario.Rol.Tester);

        //    Assert.AreEqual(1, listaT.Count());
        //    repoGestores.Verify(c => c.RepositorioUsuario.ObtenerTodos(false));
        //}

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

            List<UsuarioDTO> listaU = gestor.ObtenerTodos().ToList();

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

            string loginCorrecto = gestor.Login(this.usuarioDTOCompleto.NombreUsuario, queryableUsuarios.FirstOrDefault().Contrasenia);

            Assert.IsNotNull(loginCorrecto);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
        }

        [Test]
        public void un_usuario_no_se_puede_loguear_con_contrasenia_incorrecta()
        {
            List<Usuario> lista = new List<Usuario>();
            usuarioDTOCompleto.Token = "";
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);

            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Login(this.usuarioDTOCompleto.NombreUsuario, "password incorrecto"));
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

            string tok = gestor.Login(this.usuarioDTOCompleto.NombreUsuario, queryableUsuarios.FirstOrDefault().Contrasenia);

            Assert.IsNotEmpty(tok);
            repoGestores.Verify(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
            repoGestores.Verify(c => c.RepositorioUsuario.Modificar(It.IsAny<Usuario>()));
        }

        [Test]
        public void un_usuario_logueado_se_puede_desloguear()
        {
            usuarioDTOCompleto.Token = "asdasdasdasdasdasdasd";

            List<Usuario> lista = new List<Usuario>();
            lista.Add(usuarioCompleto);
            IQueryable<Usuario> queryableUsuarios = lista.AsQueryable();
            repoGestores.Setup(c => c.RepositorioUsuario.ObtenerPorCondicion(It.IsAny<Expression<Func<Usuario, bool>>>(), false))
                .Returns(queryableUsuarios);

            gestor.Logout(usuarioDTOCompleto.Token);

            Assert.Pass();
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

            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(new UsuarioDTO()));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_nombre_corto()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioDTOCompleto.Nombre = "ss s";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(usuarioDTOCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_nombre_largo()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioDTOCompleto.Nombre = "01234567890123456789012345";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(usuarioDTOCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_apellido_corto()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioDTOCompleto.Apellido = "ss s";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(usuarioDTOCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_apellido_largo()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioDTOCompleto.Apellido = "01234567890123456789012345";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(usuarioDTOCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_nombreUsuario_corto()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioDTOCompleto.NombreUsuario = "ss s";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(usuarioDTOCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_nombreUsuario_largo()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioDTOCompleto.NombreUsuario = "01234567890123456789012345";
            Assert.Throws<ExcepcionLargoTexto>(() => gestor.Alta(usuarioDTOCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_password_invalido()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioDTOCompleto.Contrasenia = "    ssa   a";
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(usuarioDTOCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_password_corto()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioDTOCompleto.Contrasenia = "aa";
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(usuarioDTOCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_password_largo()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioDTOCompleto.Contrasenia = "0123456789012345678901234567";
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(usuarioDTOCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }

        [Test]
        public void no_se_puede_guardar_un_usuario_con_email_invalido()
        {
            repoGestores.Setup(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>())).Returns(false);
            usuarioDTOCompleto.Email = "ssddassssas";
            Assert.Throws<ExcepcionArgumentoNoValido>(() => gestor.Alta(usuarioDTOCompleto));
            repoGestores.Verify(c => c.RepositorioUsuario.Existe(It.IsAny<Expression<Func<Usuario, bool>>>()));
        }
    }
}