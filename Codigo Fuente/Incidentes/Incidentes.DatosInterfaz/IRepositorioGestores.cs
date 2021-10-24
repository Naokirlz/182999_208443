namespace Incidentes.DatosInterfaz
{
    public interface IRepositorioGestores
    {
        IRepositorioUsuario RepositorioUsuario { get; }
        IRepositorioProyecto RepositorioProyecto { get; }
        IRepositorioIncidente RepositorioIncidente{ get; }
        IRepositorioTarea RepositorioTarea{ get; }
        void Save();
    }
}