using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Practico3.Models
{
    public class Herramientas
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }

        [ForeignKey("MarcaId")]
        public int MarcaId { get; set; } // Llave foránea para Marca
        public Marca? Marca { get; set; }
        public int CantidadTotal { get; set; } = 0;

        [DefaultValue(0)]
        public int CantidadDisponible { get; set; } = 0;

        [DefaultValue(0)]
        public int CantidadUsada { get; set; } = 0;

        [DefaultValue(0)]
        public int CantidadEnMantenimiento { get; set; } = 0;

        [DefaultValue("Disponible")]
        public string Estado { get; set; } = "Disponible"; // 'Disponible', 'En Uso', 'En Mantenimiento'

        // Método para incrementar la cantidad disponible
        public void IncrementarCantidadDisponible(int cantidad)
        {
            if (cantidad > 0)
            {
                CantidadDisponible += cantidad;
            }
            else
            {
                throw new ArgumentException("La cantidad a incrementar debe ser mayor que cero.");
            }
        }

        // Método para incrementar la cantidad usada
        public void IncrementarCantidadUsada(int cantidad)
        {
            if (cantidad > 0)
            {
                CantidadUsada += cantidad;
                CantidadDisponible -= cantidad; // Decrementar de la cantidad disponible
            }
            else
            {
                throw new ArgumentException("La cantidad a incrementar debe ser mayor que cero.");
            }
        }

        // Método para incrementar la cantidad en mantenimiento
        public void IncrementarCantidadEnMantenimiento(int cantidad)
        {
            if (cantidad > 0)
            {
                CantidadEnMantenimiento += cantidad;
                CantidadDisponible -= cantidad; // Decrementar de la cantidad disponible
            }
            else
            {
                throw new ArgumentException("La cantidad a incrementar debe ser mayor que cero.");
            }
        }

        // Método para reducir la cantidad en mantenimiento (por ejemplo, cuando se repara una herramienta)
        public void DecrementarCantidadEnMantenimiento(int cantidad)
        {
            if (cantidad > 0 && CantidadEnMantenimiento >= cantidad)
            {
                CantidadEnMantenimiento -= cantidad;
                CantidadDisponible += cantidad; // Incrementar de la cantidad disponible
            }
            else
            {
                throw new ArgumentException("La cantidad a decrementar debe ser mayor que cero y no puede exceder la cantidad en mantenimiento.");
            }
        }
    }
}
