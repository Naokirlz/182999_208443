using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Incidentes.ImportacionesTXT
{
    public class ImportacionTXT
    {
        private string _rutaFuente { get; set; }

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

                Proyecto proyecto = new Proyecto()
                {
                    Nombre = nombreProyecto
                };

                Incidente incidente = new Incidente()
                {
                    Nombre = nombreIncidente,
                    Descripcion = descripcionIncidente,
                    Version = versionIncidente
                };
                if (estadoIncidente.Equals("Activo"))
                {
                    incidente.EstadoIncidente = Incidente.Estado.Activo;
                }
                else
                {
                    incidente.EstadoIncidente = Incidente.Estado.Resuelto;
                }
                proyecto.Incidentes.Add(incidente);
                retorno.Add(proyecto);
            }
            return retorno;
        }
    }

    public class Incidente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Version { get; set; }
        public Estado EstadoIncidente { get; set; }

        public Incidente() { }

        public enum Estado
        {
            Indiferente,
            Activo,
            Resuelto
        }
    }

    public class Proyecto
    {
        public string Nombre { get; set; }
        public List<Incidente> Incidentes { get; set; }
        public Proyecto() { }
    }
}
