using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : User
    {
        protected readonly TurnoMedicoDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(TurnoMedicoDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.Where(x => x.Activo).ToList(); // Solo trae los activos
        }

        public T GetById(int id)
        {
            var entity = _dbSet.Find(id);

            if (entity == null || !entity.Activo)
            {
                throw new InvalidOperationException($"No se encontró el Usuario con Id {id}");
            }

            return entity;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);

            if (entity != null)
            {
                entity.Activo = false;
                _dbSet.Update(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IEnumerable<T> FindActive(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).Where(x => x.Activo);
        }

    }

}



