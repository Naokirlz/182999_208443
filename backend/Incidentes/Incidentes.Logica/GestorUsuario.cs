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

        public IEnumerable<Usuario> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
        public Usuario Alta(Usuario usuario)
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

        public void AltaDesarrollador(Usuario unUsuario)
        {
            bool existeUsu = this._repositorioGestor.RepositorioUsuario.Existe(u => u.NombreUsuario == unUsuario.NombreUsuario);
            if (!existeUsu)
            {
                this.Alta(unUsuario);
                _repositorioGestor.Save();
            }
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
    }
}
