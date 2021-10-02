using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
