﻿using Microsoft.EntityFrameworkCore;
using Practico3.Models;

namespace Practico3.Data
{
    public class Contextt : DbContext
    {
        public Contextt(DbContextOptions<Contextt> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<IngresoHerramienta> IngresoHerramientas { get; set; }
        public DbSet<Herramientas> Herramientas { get; set; }
        public DbSet<Asignacion> Asignaciones { get; set; }
        public DbSet<Mantenimiento> Mantenimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuracion default para parametros de Herramientas
            modelBuilder.Entity<Herramientas>()
                .Property(h => h.CantidadDisponible)
                .HasDefaultValue(0);

            modelBuilder.Entity<Herramientas>()
                .Property(h => h.CantidadUsada)
                .HasDefaultValue(0);

            modelBuilder.Entity<Herramientas>()
                .Property(h => h.CantidadEnMantenimiento)
                .HasDefaultValue(0);

            modelBuilder.Entity<Herramientas>()
                .Property(h => h.Estado)
                .HasDefaultValue("Disponible");

            // Configuracion default para parametros de Asignacion
            modelBuilder.Entity<Asignacion>()
               .Property(h => h.Estado)
               .HasDefaultValue("En uso");

            modelBuilder.Entity<Asignacion>()
                .Property(h => h.FechaAsignacion)
                .HasDefaultValue(DateTime.Now);
        }
    }
}