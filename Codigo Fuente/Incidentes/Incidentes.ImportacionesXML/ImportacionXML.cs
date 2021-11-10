using Incidentes.Dominio;
using Incidentes.Logica.DTOs;
using Incidentes.LogicaInterfaz;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Incidentes.ImportacionesXML
{
    public class ImportacionXML : IFuente
    {
        private string _rutaFuente { get; set; }

        public List<Proyecto> ImportarBugs(string rutaFuente)
        {
            _rutaFuente = rutaFuente;
            List<Proyecto> retorno = new List<Proyecto>();

            XmlRootAttribute xmlRoot = new XmlRootAttribute();
            xmlRoot.ElementName = "Empresa1";
            xmlRoot.IsNullable = true;

            XmlSerializer serializer = new XmlSerializer(typeof(EmpresaXMLDTO), xmlRoot);
            EmpresaXMLDTO proyecto = (EmpresaXMLDTO)serializer.Deserialize(new XmlTextReader(_rutaFuente));
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
    }
}
