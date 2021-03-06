namespace Incidentes.Dominio
{
    public class Incidente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ProyectoId { get; set; }
        public string Descripcion { get; set; }
        public string Version { get; set; }
        public Estado EstadoIncidente { get; set; }
        public int DesarrolladorId { get; set; }
        public int Duracion { get; set; }

        public Incidente() { }

        public enum Estado
        {
            Indiferente,
            Activo,
            Resuelto
        } 
    }
}
