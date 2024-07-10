using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProfesionalService : IProfesionalService
    {
        private readonly IProfesionalRepository _profesionalRepository;

        public ProfesionalService(IProfesionalRepository profesionalRepository)
        {
            _profesionalRepository = profesionalRepository;
        }

        public IEnumerable<Profesional> GetAllProfesionales()
        {
            return _profesionalRepository.GetAll();
        }

        public Profesional GetProfesionalById(int id)
        {
            return _profesionalRepository.GetById(id);
        }

        public void AddProfesional(Profesional profesional)
        {
            // Verifica si el profesional ya existe por DNI y está activo
            var existingProfesional = _profesionalRepository.FindActive(p => p.DNI == profesional.DNI).FirstOrDefault();

            if (existingProfesional != null)
            {
                throw new InvalidOperationException("El profesional ya existe.");
            }

            _profesionalRepository.Add(profesional);
        }

        public void UpdateProfesional(Profesional profesional)
        {
            // Verifica si otro profesional con el mismo DNI ya existe y está activo
            var existingProfesional = _profesionalRepository.FindActive(p => p.DNI == profesional.DNI && p.Id != profesional.Id).FirstOrDefault();
            
            if (existingProfesional != null)
            {
                throw new InvalidOperationException("Ya existe otro profesional con el mismo DNI.");
            }

            _profesionalRepository.Update(profesional);
        }

        public void DeleteProfesional(int id)
        {
            _profesionalRepository.Delete(id);
        }

    }

}
