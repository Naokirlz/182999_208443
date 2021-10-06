using Incidentes.DatosInterfaz;
using Incidentes.Dominio;
using Incidentes.Logica.DTOs;
using Incidentes.Logica.Excepciones;
using Incidentes.LogicaInterfaz;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Incidentes.Logica
{
    public class FuenteXML : IFuente
    {
        IRepositorioGestores _repositorioGestor;
        private string _rutaFuente { get; set; }

        private const string elemento_no_existe = "El elemento no existe";

        public FuenteXML(IRepositorioGestores repositorioGestores, string rutaFuente)
        {
            if (!File.Exists(rutaFuente))
            {
                throw new ExcepcionElementoNoExiste(elemento_no_existe);
            }
            _repositorioGestor = repositorioGestores;
            _rutaFuente = rutaFuente;
        }

        public void ImportarBugs()
        {

            XmlRootAttribute xmlRoot = new XmlRootAttribute();
            xmlRoot.ElementName = "Empresa1";
            xmlRoot.IsNullable = true;

            XmlSerializer serializer = new XmlSerializer(typeof(EmpresaXMLDTO), xmlRoot);
            EmpresaXMLDTO proyecto = (EmpresaXMLDTO)serializer.Deserialize(new XmlTextReader(_rutaFuente));
            Proyecto buscado = _repositorioGestor.RepositorioProyecto.ObtenerPorCondicion(p => p.Nombre == proyecto.Proyecto, true).FirstOrDefault();
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
                _repositorioGestor.RepositorioIncidente.Alta(incidente);
                buscado.Incidentes.Add(incidente);
            }
            _repositorioGestor.Save();
        }
    }
}
