using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class TurnoMedicoDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Profesional> Profesionales { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Turno> Turnos { get; set; }

        public TurnoMedicoDbContext(DbContextOptions<TurnoMedicoDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración para la entidad User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            // Configuración para la entidad Profesional
            modelBuilder.Entity<Profesional>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Profesional>()
                .HasMany(p => p.Pacientes)
                .WithOne()
                .HasForeignKey(p => p.ProfesionalId)
                .OnDelete(DeleteBehavior.Restrict); // Eliminacion de Profesional NO elimina Paciente

            modelBuilder.Entity<Profesional>()
                .HasMany(p => p.Turnos)
                .WithOne()
                .HasForeignKey(t => t.ProfesionalId)
                .OnDelete(DeleteBehavior.Restrict); // Eliminacion de Profesional NO elimina Paciente

            // Configuración para la entidad Paciente
            modelBuilder.Entity<Paciente>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Paciente>()
                .HasMany(p => p.Turnos)
                .WithOne()
                .HasForeignKey(t => t.PacienteId)
                .OnDelete(DeleteBehavior.Restrict); // Relación Paciente-Turnos

            // Configuración para la entidad Turno
            modelBuilder.Entity<Turno>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Paciente)
                .WithMany(p => p.Turnos)
                .HasForeignKey(t => t.PacienteId)
                .OnDelete(DeleteBehavior.Restrict); // Relación Turno-Paciente

            modelBuilder.Entity<Turno>()
                .HasOne(t => t.Profesional)
                .WithMany(p => p.Turnos)
                .HasForeignKey(t => t.ProfesionalId)
                .OnDelete(DeleteBehavior.Restrict); // Relación Turno-Profesional

            base.OnModelCreating(modelBuilder);
        }

    }
}
