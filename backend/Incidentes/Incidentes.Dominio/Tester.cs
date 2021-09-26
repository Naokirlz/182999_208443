using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidentes.Dominio
{
    public class Tester:Usuario
    {
        public virtual List<Proyecto> proyectos { get; set; }
    }
}
