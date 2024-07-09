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
            _pacienteRepository.Add(paciente);
        }

        public void UpdatePaciente(Paciente paciente)
        {
            _pacienteRepository.Update(paciente);
        }

        public void DeletePaciente(int id)
        {
            _pacienteRepository.Delete(id);
        }

    }

}
