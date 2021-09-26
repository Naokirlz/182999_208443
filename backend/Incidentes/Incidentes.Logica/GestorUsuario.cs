using System;
using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;

namespace Incidentes.Logica
{
    public class GestorUsuario : ILogicaUsuario
    {
        IRepositorioGestores _repositorioGestor;

        public GestorUsuario(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }


        public Usuario Modificar(int id, Usuario entity)
        {
            throw new NotImplementedException();
        }
        public bool Baja(int id)
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
            if (coincide && usuario.Token == "")
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

        public void AltaDesarrollador(string token, Usuario unDesarrollador)
        {
            //chequear que existe administrador
            bool existeUsu = this._repositorioGestor.RepositorioUsuario.Existe(u => u.Token == token);

            if (existeUsu)
            {
                //verificar administrador
                Usuario usuLogueado = this._repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(u => u.Token == token, false).FirstOrDefault();
                if (usuLogueado.GetType() == new Administrador().GetType())
                {
                    //chequear no existe desarrollador
                    existeUsu = this._repositorioGestor.RepositorioUsuario.Existe(u => u.NombreUsuario == unDesarrollador.NombreUsuario);
                    if (!existeUsu)
                    {
                        //crear desarrrollador
                        this.Alta(unDesarrollador);
                        _repositorioGestor.Save();
                    }
                }
            }
        }
    }
}
