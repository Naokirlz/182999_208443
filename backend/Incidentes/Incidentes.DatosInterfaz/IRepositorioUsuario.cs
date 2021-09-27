using Incidentes.Dominio;


namespace Incidentes.DatosInterfaz
{
    public interface IRepositorioUsuario : IRepositorioBase<Usuario>
    {
        public int CantidadDeIncidentesResueltosPorUnDesarrollador(int id);
    }
}
