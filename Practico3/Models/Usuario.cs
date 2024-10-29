namespace Practico3.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }

        // campo solo de lectura
        public int HerramientasAsignadas { get; private set; }

        public List<Asignacion> Asignaciones { get; set; }

        // metodo para actualizar HerramientasAsignadas basado en la cantidad de asignaciones.
        public void ActualizarHerramientasAsignadas()
        {
            HerramientasAsignadas = Asignaciones.Count(a => a.FechaDevolucion == null);
        }
    }
}
