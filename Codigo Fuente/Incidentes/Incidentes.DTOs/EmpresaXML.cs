using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.DTOs
{
    public class EmpresaXML
    {
        public string Proyecto { get; set; }
        public List<Bug> Bugs { get; set; }

        public EmpresaXML()
        {
            Bugs = new List<Bug>();
        }
    }
}
