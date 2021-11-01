
using System.Collections.Generic;

namespace Incidentes.LogicaInterfaz
{
    public interface ILogicaImportaciones
    {
        public void ImportarBugs(string rutaFuente, string rutaBinario, int usuarioId);
        public List<string> ListarPlugins();
    }
}