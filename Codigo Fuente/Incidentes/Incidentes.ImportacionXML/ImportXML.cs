using Incidentes.DTOs;
using Incidentes.LogicaInterfaz;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Incidentes.ImportacionXML
{
    public class ImportXML : IFuente
    {
        public List<ProyectoDTO> ImportarBugs(string rutaFuente)
        {
            List<ProyectoDTO> retorno = new List<ProyectoDTO>();

            XmlRootAttribute xmlRoot = new XmlRootAttribute();
            xmlRoot.ElementName = "Proyecto";
            xmlRoot.IsNullable = true;

            ProyectoDTO proyecto;

            XmlSerializer serializer = new XmlSerializer(typeof(ProyectoDTO), xmlRoot);
            proyecto = (ProyectoDTO)serializer.Deserialize(new XmlTextReader(rutaFuente));
            retorno.Add(proyecto);
            return retorno;
        }
    }
}
