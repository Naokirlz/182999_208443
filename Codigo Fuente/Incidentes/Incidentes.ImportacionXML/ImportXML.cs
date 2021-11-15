using Incidentes.DTOs;
using Incidentes.LogicaInterfaz;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Incidentes.ImportacionXML
{
    public class ImportXML : IFuente
    {
        public EmpresaXML empresa;
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
    }
}
