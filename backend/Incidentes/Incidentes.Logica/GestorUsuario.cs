using System;
using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;
using Incidentes.Logica.Excepciones;

namespace Incidentes.Logica
{
    public class GestorUsuario : ILogicaUsuario
    {
        IRepositorioGestores _repositorioGestor;
        private const string argumento_nulo = "El argumento no puede ser nulo";
        private const string elemento_no_existe = "El elemento no existe";
        private const string elemento_ya_existe = "Un elemento con similares atributos ya existe";
        private const int largo_maximo_texto_corto = 25;
        private const int largo_minimo_texto_corto = 5;

        public GestorUsuario(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }


        public Usuario Modificar(int id, Usuario entity)
        {
            throw new NotImplementedException();
        }
        public void Baja(int id)
        {
            throw new NotImplementedException();
        }

        public Usuario Obtener(int id)
        {
            bool existe = _repositorioGestor.RepositorioUsuario.Existe(c => c.Id == id);
            if (!existe) throw new ExcepcionElementoNoExiste(elemento_no_existe);
            var usuario = _repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(a => a.Id == id, trackChanges: false).FirstOrDefault();
            return usuario;
        }

        public IEnumerable<Usuario> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
        public Usuario Alta(Usuario usuario)
        {
            if (usuario == null) throw new ExcepcionArgumentoNoValido(argumento_nulo);
            bool existe = _repositorioGestor.RepositorioUsuario.Existe(c => c.NombreUsuario == usuario.NombreUsuario) ||
                _repositorioGestor.RepositorioUsuario.Existe(c => c.Email == usuario.Email);
            if (existe) throw new ExcepcionArgumentoNoValido(elemento_ya_existe);

            Validaciones.ValidarLargoTexto(usuario.Nombre, largo_maximo_texto_corto, largo_minimo_texto_corto, "Nombre");
            Validaciones.ValidarLargoTexto(usuario.Apellido, largo_maximo_texto_corto, largo_minimo_texto_corto, "Apellido");
            Validaciones.ValidarLargoTexto(usuario.NombreUsuario, largo_maximo_texto_corto, largo_minimo_texto_corto, "Nombre de usuario");
            Validaciones.ValidarPassword(usuario.Contrasenia);
            Validaciones.ValidarEmail(usuario.Email);

            _repositorioGestor.RepositorioUsuario.Alta(usuario);
            _repositorioGestor.Save();


            return usuario;
        }

        public bool Login(string nombreUSuario, string password)
        {
            Usuario usuario = this.ObtenerPorNombreUsuario(nombreUSuario);
            bool coincide = usuario.Contrasenia == password;
            if (coincide)
            {
                GenerarToken(usuario);
            }
            return coincide;
        }

        public void Logout(string tokenUsuario)
        {
            Usuario usuario = this._repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(c => c.Token == tokenUsuario, false).FirstOrDefault();
            usuario.Token = "";
            this._repositorioGestor.RepositorioUsuario.Modificar(usuario);
            _repositorioGestor.Save();
        }

        private Usuario ObtenerPorNombreUsuario(string nombreUsuario) {
            Usuario buscado = this._repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(c => c.NombreUsuario == nombreUsuario, false).FirstOrDefault();
            return buscado;
        }

        private void GenerarToken(Usuario usuario)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwzyz!@#$%^&*0123456789";
            bool existe = false;
            string token = "";
            do
            {
                token = new string(Enumerable.Repeat(chars, 32).Select(s => s[random.Next(s.Length)]).ToArray());
                existe = this._repositorioGestor.RepositorioUsuario.Existe(s => s.Token == token);
            } while (existe);
            usuario.Token = token;
            this._repositorioGestor.RepositorioUsuario.Modificar(usuario);
            _repositorioGestor.Save();
        }

        public int CantidadDeIncidentesResueltosPorUnDesarrollador(int idDesarrollador)
        {
            return _repositorioGestor.RepositorioUsuario.CantidadDeIncidentesResueltosPorUnDesarrollador(idDesarrollador);
        }

        public List<Incidente> ListaDeIncidentesDeLosProyectosALosQuePertenece(int idUsuario, string nombreProyecto, Incidente incidente)
        {
            return _repositorioGestor.RepositorioUsuario.ListaDeIncidentesDeLosProyectosALosQuePertenece(idUsuario, nombreProyecto, incidente);
        }

        public IQueryable<Proyecto> ListaDeProyectosALosQuePertenece(int idUsuario, int idDesarrollador) {
            return _repositorioGestor.RepositorioUsuario.ListaDeProyectosALosQuePertenece(idUsuario);
        }

        public List<Desarrollador> ObtenerDesarrolladores()
        {
            List<Usuario> lista = new List<Usuario>();
            List<Desarrollador> desarrolladores = new List<Desarrollador>();

            lista = _repositorioGestor.RepositorioUsuario.ObtenerTodos(false).ToList();

            foreach (Usuario u in lista)
                if (u.GetType() == new Desarrollador().GetType())
                    desarrolladores.Add((Desarrollador) u);
            
            return desarrolladores;
        }

        public Desarrollador ObtenerDesarrollador(int idDesarrollador)
        {
            Usuario u = Obtener(idDesarrollador);
            if(u.GetType() != new Desarrollador().GetType()) throw new ExcepcionElementoNoExiste(elemento_no_existe);
            return (Desarrollador) u;
        }

        public List<Tester> ObtenerTesters() {
            List<Usuario> lista = new List<Usuario>();
            List<Tester> testers = new List<Tester>();

            lista = _repositorioGestor.RepositorioUsuario.ObtenerTodos(false).ToList();

            foreach (Usuario u in lista)
                if (u.GetType() == new Tester().GetType())
                    testers.Add((Tester)u);

            return testers;
        }
        public Tester ObtenerTester(int idTester) {
            Usuario u = Obtener(idTester);
            if (u.GetType() != new Tester().GetType()) throw new ExcepcionElementoNoExiste(elemento_no_existe);
            return (Tester)u;
        }
    }
}
