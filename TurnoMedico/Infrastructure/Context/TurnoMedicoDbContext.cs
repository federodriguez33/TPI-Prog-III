using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class TurnoMedicoDbContext : DbContext
    {
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Turno> Turnos { get; set; }

        public TurnoMedicoDbContext(DbContextOptions<TurnoMedicoDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configuración de Profesional
            modelBuilder.Entity<Profesional>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Profesional>()
                .HasMany(p => p.Turnos)
                .WithOne()
                .HasForeignKey(t => t.ProfesionalId)
                .OnDelete(DeleteBehavior.Cascade); // Eliminacion de Profesional elimina sus Turnos

            // Configuración de Paciente
            modelBuilder.Entity<Paciente>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Turnos)
                .WithOne()
                .HasForeignKey(t => t.PacienteId)
                .OnDelete(DeleteBehavior.Cascade); // Eliminacion de Paciente elimina sus Turnos

            // Configuración de Turno
            modelBuilder.Entity<Turno>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Paciente)
                .WithMany(p => p.Turnos)
                .HasForeignKey(t => t.PacienteId)
                .OnDelete(DeleteBehavior.Restrict); // Eliminacion de Turno NO elimina Paciente

            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Profesional)
                .WithMany(p => p.Turnos)
                .HasForeignKey(t => t.ProfesionalId)
                .OnDelete(DeleteBehavior.Restrict); // Eliminacion de Turno NO elimina Profesional

            base.OnModelCreating(modelBuilder);
        }

    }
}
