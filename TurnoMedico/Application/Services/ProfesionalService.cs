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
            _profesionalRepository.Add(profesional);
        }

        public void UpdateProfesional(Profesional profesional)
        {
            _profesionalRepository.Update(profesional);
        }

        public void DeleteProfesional(int id)
        {
            _profesionalRepository.Delete(id);
        }

    }

}
