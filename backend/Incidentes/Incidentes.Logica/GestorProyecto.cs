using System;
using System.Collections.Generic;
using Incidentes.Dominio;
using System.Linq;
using Incidentes.DatosInterfaz;
using Incidentes.LogicaInterfaz;
using Incidentes.Logica.Interfaz;
using Incidentes.Logica.Excepciones;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Incidentes.Logica.DTOs;

namespace Incidentes.Logica
{
    public class GestorProyecto : ILogicaProyecto
    {
        IRepositorioGestores _repositorioGestor;
        private const string argumento_nulo = "El argumento no puede ser nulo";
        private const string elemento_no_existe = "El elemento no existe";
        private const string elemento_ya_existe = "Un elemento con similares atributos ya existe"; 
        private const int largo_maximo_nombre = 25;
        private const int largo_minimo_nombre = 5;

        public GestorProyecto(IRepositorioGestores repositorioGestores)
        {
            _repositorioGestor = repositorioGestores;
        }

        public void AgregarDesarrolladorAProyecto(int desarrollador, int idProyecto)
        {
            bool existeProyecto =_repositorioGestor.RepositorioProyecto.Existe(p => p.Id == idProyecto);
            if (existeProyecto)
            {
                Usuario aAgregar = _repositorioGestor.RepositorioUsuario.ObtenerPorCondicion(d => d.Id == desarrollador, true).FirstOrDefault();

                if(aAgregar != null)
                {
                    Proyecto aModificar = _repositorioGestor.RepositorioProyecto.ObtenerPorCondicion(p => p.Id == idProyecto, true).FirstOrDefault();
                    aModificar.Desarrolladores.Add((Desarrollador)aAgregar);

                    _repositorioGestor.RepositorioProyecto.Modificar(aModificar);
                    _repositorioGestor.Save();

                }
                               
            }

        }

        public Proyecto Alta(Proyecto entity)
        {
            if (entity == null) throw new ExcepcionArgumentoNoValido(argumento_nulo);
            bool existe = _repositorioGestor.RepositorioProyecto.Existe(c => c.Nombre == entity.Nombre);
            if (existe) throw new ExcepcionArgumentoNoValido(elemento_ya_existe);

            Validaciones.ValidarLargoTexto(entity.Nombre, largo_maximo_nombre, largo_minimo_nombre, "Nombre Proyecto");

            _repositorioGestor.RepositorioProyecto.Alta(entity);
            _repositorioGestor.Save();

            return entity;
        }

        public void Baja(int id)
        {
            Proyecto aEliminar = Obtener(id);

            _repositorioGestor.RepositorioProyecto.Eliminar(aEliminar);
            _repositorioGestor.Save();

        }

        public Proyecto Modificar(int id, Proyecto entity)
        {
            if (entity == null) throw new ExcepcionArgumentoNoValido(argumento_nulo);

            Proyecto aModificar = Obtener(id);

            if(aModificar.Nombre != entity.Nombre)
            {
                bool existe = _repositorioGestor.RepositorioProyecto.Existe(c => c.Nombre == entity.Nombre);
                if (existe) throw new ExcepcionArgumentoNoValido(elemento_ya_existe);
                Validaciones.ValidarLargoTexto(entity.Nombre, largo_maximo_nombre, largo_minimo_nombre, "Nombre Proyecto");
            }

            aModificar.Nombre = entity.Nombre;
            aModificar.Desarrolladores = entity.Desarrolladores;
            aModificar.Testers = entity.Testers;
            aModificar.Incidentes = entity.Incidentes;

            _repositorioGestor.RepositorioProyecto.Modificar(aModificar);
            _repositorioGestor.Save();
            return aModificar;
        }

        public Proyecto Obtener(int id)
        {
            bool existe = _repositorioGestor.RepositorioProyecto.Existe(c => c.Id == id);
            if (!existe) throw new ExcepcionElementoNoExiste(elemento_no_existe);
            Proyecto aObtener= _repositorioGestor.RepositorioProyecto.ObtenerPorCondicion(c => c.Id == id, true).FirstOrDefault();
            return aObtener;
        }

        public IEnumerable<Proyecto> ObtenerTodos()
        {
            return _repositorioGestor.RepositorioProyecto.ObtenerTodos(false);
        }

        public bool VerificarUsuarioPerteneceAlProyecto(int idUsuario, int idProyecto)
        {
            return _repositorioGestor.RepositorioProyecto.VerificarUsuarioPerteneceAlProyecto(idUsuario, idProyecto);
        }

        public void ImportarBugs(string rutaFuente)
        {
            IFuente fuenteXML = new FuenteXML(_repositorioGestor, rutaFuente);
            fuenteXML.ImportarBugs();
        }

        public void ImportarBugsTXT(string rutaFuente)
        {
            if (!File.Exists(rutaFuente))
            {
                throw new ExcepcionElementoNoExiste(elemento_no_existe);
            }

            List<string> lineas = File.ReadAllLines(rutaFuente).ToList();
            int id_bug = 30;
            int nombre_bug = id_bug + 4 + 1;
            int descripcion_bug = nombre_bug + 60 + 1;
            int version_bug = descripcion_bug + 150 + 1;
            int estado_bug = version_bug + 10 + 1;
            foreach (var item in lineas)
            {
                string nombreProyecto = item.Substring(0, 30).Trim();
                string nombreIncidente = item.Substring(nombre_bug, 60).Trim();
                string descripcionIncidente = item.Substring(descripcion_bug, 140).Trim();
                string versionIncidente = item.Substring(version_bug, 10).Trim();
                string estadoIncidente = item.Substring(estado_bug).Trim();

                Proyecto proyecto = _repositorioGestor.RepositorioProyecto.ObtenerPorCondicion(p => p.Nombre == nombreProyecto, true).FirstOrDefault();

                Incidente incidente = new Incidente()
                {
                    Nombre = nombreIncidente,
                    Descripcion = descripcionIncidente,
                    Version = (int)double.Parse(versionIncidente)
                };
                if (estadoIncidente.Equals("Activo"))
                {
                    incidente.EstadoIncidente = Incidente.Estado.Activo;
                }
                else
                {
                    incidente.EstadoIncidente = Incidente.Estado.Resuelto;
                }
                _repositorioGestor.RepositorioIncidente.Alta(incidente);
                proyecto.Incidentes.Add(incidente);
                _repositorioGestor.Save();
            }
        }
    }
}
