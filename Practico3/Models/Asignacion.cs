namespace Practico3.Models
{
    public class Asignacion
    {
        public int Id { get; set; }
        public int HerramientaId { get; set; }
        public Herramienta Herramienta { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public string Estado { get; set; } // 'Asignado', 'En Mantenimiento', etc.
    }
}
