namespace Practico3.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }

        // Campo privado
        public int HerramientasAsignadas { get; private set; } = 0;

        // Método para incrementar herramientas asignadas
        public void IncrementarHerramientasAsignadas()
        {
            HerramientasAsignadas++;
        }
    }
}