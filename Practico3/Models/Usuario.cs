namespace Practico3.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int HerramientasAsignadas { get; set; } // Máximo permitido: 3
        public List<Asignacion> Asignaciones { get; set; } // Relación con asignaciones de herramientas
    }
}
