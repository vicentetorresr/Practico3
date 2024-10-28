namespace Practico3.Models
{
    public class Mantenimiento
    {
        public int Id { get; set; }
        public int HerramientaId { get; set; }
        public Herramienta Herramienta { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public bool EstaEnMantenimiento => FechaDevolucion == null;
    }
}
