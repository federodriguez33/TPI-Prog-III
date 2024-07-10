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
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public IEnumerable<Paciente> GetAllPacientes()
        {
            return _pacienteRepository.GetAll();
        }

        public Paciente GetPacienteById(int id)
        {
            return _pacienteRepository.GetById(id);
        }

        public void AddPaciente(Paciente paciente)
        {
            // Verifica si el paciente ya existe por DNI y está activo
            var existingPaciente = _pacienteRepository.FindActive(p => p.DNI == paciente.DNI).FirstOrDefault();

            if (existingPaciente != null)
            {
                throw new InvalidOperationException("El paciente ya existe.");
            }

            _pacienteRepository.Add(paciente);
        }

        public void UpdatePaciente(Paciente paciente)
        {
            // Verifica si otro paciente con el mismo DNI ya existe y está activo
            var existingPaciente = _pacienteRepository.FindActive(p => p.DNI == paciente.DNI && p.Id != paciente.Id).FirstOrDefault();

            if (existingPaciente != null)
            {
                throw new InvalidOperationException("Ya existe otro paciente con el mismo DNI.");
            }

            _pacienteRepository.Update(paciente);
        }

        public void DeletePaciente(int id)
        {
            _pacienteRepository.Delete(id);
        }

    }

}
