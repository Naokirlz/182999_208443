using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Incidentes.ImportacionXML
{
    public class ImportXML
    {
        public List<Proyecto> ImportarBugs(string rutaFuente)
        {
            List<Proyecto> retorno = new List<Proyecto>();

            XmlRootAttribute xmlRoot = new XmlRootAttribute();
            xmlRoot.ElementName = "Empresa1";
            xmlRoot.IsNullable = true;

            XmlSerializer serializer = new XmlSerializer(typeof(EmpresaXML), xmlRoot);
            EmpresaXML proyecto = (EmpresaXML)serializer.Deserialize(new XmlTextReader(rutaFuente));
            Proyecto pro = new Proyecto()
            {
                Nombre = proyecto.Proyecto
            };

            foreach (Bug b in proyecto.Bugs)
            {
                Incidente incidente = new Incidente()
                {
                    Nombre = b.Nombre,
                    Descripcion = b.Descripcion,
                    Version = b.Version
                };
                if (b.Estado.Equals("Activo"))
                {
                    incidente.EstadoIncidente = Incidente.Estado.Activo;
                }
                else
                {
                    incidente.EstadoIncidente = Incidente.Estado.Resuelto;
                }
                pro.Incidentes.Add(incidente);

            }
            retorno.Add(pro);
            return retorno;
        }

        private class EmpresaXML
        {
            public string Proyecto { get; set; }
            public List<Bug> Bugs { get; set; }

            public EmpresaXML()
            {
                Bugs = new List<Bug>();
            }
        }
        private class Bug
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public string Version { get; set; }
            public string Estado { get; set; }

            public Bug() { }
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
}
