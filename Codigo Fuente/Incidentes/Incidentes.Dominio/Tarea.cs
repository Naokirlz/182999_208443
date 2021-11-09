namespace Incidentes.Dominio
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Costo { get; set; }
        public int Duracion { get; set; }
        public int ProyectoId { get; set; }
        public Tarea() { }
    }
}
