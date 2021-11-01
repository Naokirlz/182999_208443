using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.Excepciones;
using Incidentes.LogicaInterfaz;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Incidentes.Logica
{
    public class FuenteTXT : IFuente
    {
        IRepositorioGestores _repositorioGestor;
        private string _rutaFuente { get; set; }
        private int _usuarioId { get; set; }

        private const string elemento_no_existe = "El elemento no existe";

        public FuenteTXT(IRepositorioGestores repositorioGestores, string rutaFuente, int usuarioId)
        {
            if (!File.Exists(rutaFuente))
            {
                throw new ExcepcionElementoNoExiste(elemento_no_existe);
            }
            _repositorioGestor = repositorioGestores;
            _rutaFuente = rutaFuente;
            _usuarioId = usuarioId;
        }

        public List<Proyecto> ImportarBugs(string rutaFuente)
        {
            _rutaFuente = rutaFuente;
            List<Proyecto> retorno = new List<Proyecto>();
            List<string> lineas = File.ReadAllLines(_rutaFuente).ToList();
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

                // Proyecto proyecto = _repositorioGestor.RepositorioProyecto.ObtenerPorCondicion(p => p.Nombre == nombreProyecto, true).FirstOrDefault();
                Proyecto proyecto = new Proyecto()
                {
                    Nombre = nombreProyecto
                };

                Incidente incidente = new Incidente()
                {
                    Nombre = nombreIncidente,
                    Descripcion = descripcionIncidente,
                    Version = versionIncidente,
                    UsuarioId = _usuarioId
                };
                if (estadoIncidente.Equals("Activo"))
                {
                    incidente.EstadoIncidente = Incidente.Estado.Activo;
                }
                else
                {
                    incidente.EstadoIncidente = Incidente.Estado.Resuelto;
                }
                //_repositorioGestor.RepositorioIncidente.Alta(incidente);
                //proyecto.Incidentes.Add(incidente);
                //_repositorioGestor.Save();
                proyecto.Incidentes.Add(incidente);
                retorno.Add(proyecto);
            }
            return retorno;
        }
    }
}
