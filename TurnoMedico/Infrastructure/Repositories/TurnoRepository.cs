using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TurnoRepository : ITurnoRepository
    {
        protected readonly TurnoMedicoDbContext _context;
        protected readonly DbSet<Turno> _dbSet;

        public TurnoRepository(TurnoMedicoDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Turno>();
        }

        public IEnumerable<Turno> GetAll()
        {
            return _dbSet.ToList();
        }

        public Turno GetById(int id)
        {
            var turno = _dbSet.Find(id);

            if (turno == null)
            {
                throw new Exception($"Turno con Id {id} no encontrado.");
            }

            return turno;
        }

        public void Add(Turno turno)
        {
            _dbSet.Add(turno);
            _context.SaveChanges();
        }

        public void Update(Turno turno)
        {
            _dbSet.Update(turno);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var turno = _dbSet.Find(id);
            if (turno != null)
            {
                _dbSet.Remove(turno);
                _context.SaveChanges();
            }
        }

        public bool IsTurnoAvailable(Turno turno)
        {
            return !_dbSet.Any(t => t.Id != turno.Id && t.ProfesionalId == turno.ProfesionalId && t.FechaHora == turno.FechaHora);
        }

    }

}
