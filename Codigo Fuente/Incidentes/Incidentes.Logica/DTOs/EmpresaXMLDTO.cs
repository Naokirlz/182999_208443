using System.Collections.Generic;

namespace Incidentes.Logica.DTOs
{
    public class EmpresaXMLDTO
    {
        public string Proyecto { get; set; }
        public List<Bug> Bugs { get; set; }

        public EmpresaXMLDTO()
        {

            Bugs = new List<Bug>();

        }
    }
}
