using Incidentes.DatosInterfaz;
using Incidentes.Dominio;

namespace Incidentes.Datos
{
    public class RepositorioTareaEntity : RepositorioBase<Tarea>, IRepositorioTarea
    {
        public RepositorioTareaEntity(Contexto contextoRepositorio) : base(contextoRepositorio)
        {
        }
    }
}
