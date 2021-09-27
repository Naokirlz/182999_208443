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
        private const string acceso_no_autorizado = "Acceso no autorizado";
        private const string acceso_prohibido = "Acceso prohibido";

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
            var usuario = _repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(a => a.Id == id, trackChanges: false).FirstOrDefault();
            return usuario;
        }

        public IEnumerable<Usuario> ObtenerTodos(string token)
        {
            throw new NotImplementedException();
        }
        public Usuario Alta(string token, Usuario usuario)
        {
            
            if (usuario == null)
            {
             throw new Exception(); 
            }
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

        public void AltaDesarrollador(string token, Usuario unUsuario)
        {
            //chequear que existe administrador
            bool existeUsu = this._repositorioGestor.RepositorioUsuario.Existe(u => u.Token == token);

            if (existeUsu)
            {
                //verificar administrador
                Usuario usuLogueado = this._repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(u => u.Token == token, false).FirstOrDefault();
                if (usuLogueado.GetType() == new Administrador().GetType())
                {
                    existeUsu = this._repositorioGestor.RepositorioUsuario.Existe(u => u.NombreUsuario == unUsuario.NombreUsuario);
                    if (!existeUsu)
                    {
                        this.Alta(token, unUsuario);
                        _repositorioGestor.Save();
                    }
                }
            }
        }

        public int CantidadDeIncidentesResueltosPorUnDesarrollador(string token, int idDesarrollador)
        {
            UsuarioAutenticado(token);
            Usuario usuario = _repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(u => u.Token == token, false).FirstOrDefault();

            bool correctoTipo = VerificarTipo(new Administrador().GetType(), usuario.GetType());

            if (!correctoTipo)
                throw new ExcepcionAccesoNoAutorizado(acceso_prohibido);

            return _repositorioGestor.RepositorioUsuario.CantidadDeIncidentesResueltosPorUnDesarrollador(idDesarrollador);
        }

        private bool VerificarTipo(Type tipoEsperado, Type tipoAComparar)
        {
            return tipoEsperado == tipoAComparar;
        }

        public List<Incidente> ListaDeIncidentesDeLosProyectosALosQuePertenece(string token, string nombreProyecto, Incidente incidente)
        {
            UsuarioAutenticado(token);
            Usuario usuario = _repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(u => u.Token == token, false).FirstOrDefault();

            return _repositorioGestor.RepositorioUsuario.ListaDeIncidentesDeLosProyectosALosQuePertenece(usuario.Id, nombreProyecto, incidente);
        }

        public IQueryable<Proyecto> ListaDeProyectosALosQuePertenece(string token, int idDesarrollador) {
            UsuarioAutenticado(token);
            Usuario usuario = _repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(u => u.Token == token, false).FirstOrDefault();

            return _repositorioGestor.RepositorioUsuario.ListaDeProyectosALosQuePertenece(usuario.Id);
        }

        public void UsuarioAutenticado(string token)
        {
            bool existeUsu = this._repositorioGestor.RepositorioUsuario.Existe(u => u.Token == token);
            if (!existeUsu)
                throw new ExcepcionAccesoNoAutorizado(acceso_no_autorizado);
        }
    }
}
