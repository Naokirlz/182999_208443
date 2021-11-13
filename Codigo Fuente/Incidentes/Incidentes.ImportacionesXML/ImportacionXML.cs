
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Incidentes.ImportacionesXML
{
    public class ImportacionXML
    {
        private string _rutaFuente { get; set; }

        public List<Proyecto> ImportarBugs(string rutaFuente)
        {
            _rutaFuente = rutaFuente;
            List<Proyecto> retorno = new List<Proyecto>();

            XmlRootAttribute xmlRoot = new XmlRootAttribute();
            xmlRoot.ElementName = "Empresa1";
            xmlRoot.IsNullable = true;

            XmlSerializer serializer = new XmlSerializer(typeof(EmpresaXML), xmlRoot);
            EmpresaXML proyecto = (EmpresaXML)serializer.Deserialize(new XmlTextReader(_rutaFuente));
            Proyecto pro = new Proyecto()
            {
                Nombre = proyecto.Proyecto
            };

            foreach (Incidente i in proyecto.Incidentes)
            {
                pro.Incidentes.Add(i);
            }
            retorno.Add(pro);
            return retorno;
        }
    }

    public class EmpresaXML
    {
        public string Proyecto { get; set; }
        public List<Incidente> Incidentes { get; set; }

        public EmpresaXML()
        {
            Incidentes = new List<Incidente>();
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
