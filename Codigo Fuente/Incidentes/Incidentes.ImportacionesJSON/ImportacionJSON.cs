using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Incidentes.ImportacionesJSON
{
    public class ImportacionJSON
    {
        public List<Proyecto> ImportarBugs(string rutaFuente)
        {
            List<Proyecto> retorno = new List<Proyecto>();

            StreamReader jsonFile = new StreamReader(rutaFuente);
            string jsonString = jsonFile.ReadToEnd();
            Proyecto pro = JsonSerializer.Deserialize<Proyecto>(jsonString);

            retorno.Add(pro);
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
