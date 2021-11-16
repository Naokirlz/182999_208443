using Incidentes.DTOs;
using Incidentes.LogicaInterfaz;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Incidentes.ImportacionXML
{
    public class ImportXML : IFuente
    {
        private EmpresaXML empresa;
        public List<ProyectoDTO> ImportarBugs(string rutaFuente)
        {
            List<ProyectoDTO> retorno = new List<ProyectoDTO>();

            XmlRootAttribute xmlRoot = new XmlRootAttribute();
            xmlRoot.ElementName = "Empresa1";
            xmlRoot.IsNullable = true;

            XmlSerializer serializer = new XmlSerializer(typeof(EmpresaXML), xmlRoot);
            empresa = (EmpresaXML)serializer.Deserialize(new XmlTextReader(rutaFuente));
            ProyectoDTO pro = new ProyectoDTO()
            {
                Nombre = empresa.Proyecto
            };

            foreach (Bug b in empresa.Bugs)
            {
                IncidenteDTO incidente = new IncidenteDTO()
                {
                    Nombre = b.Nombre,
                    Descripcion = b.Descripcion,
                    Version = b.Version
                };
                if (b.Estado.Equals("Activo"))
                {
                    incidente.EstadoIncidente = IncidenteDTO.Estado.Activo;
                }
                else
                {
                    incidente.EstadoIncidente = IncidenteDTO.Estado.Resuelto;
                }
                pro.Incidentes.Add(incidente);

            }
            retorno.Add(pro);
            return retorno;
        }

        internal class EmpresaXML
        {
            public string Proyecto { get; set; }
            public List<Bug> Bugs { get; set; }

            public EmpresaXML()
            {
                Bugs = new List<Bug>();
            }
        }

        internal class Bug
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public string Version { get; set; }
            public string Estado { get; set; }

            public Bug() { }
        }
    }
}
