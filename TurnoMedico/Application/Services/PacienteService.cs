using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PacienteService
    {
        public Paciente? GetByName(string name)
        {
            return _studentRepository.Get(name);
        }

        public List<Paciente> GetAll()
        {
            return _studentRepository.Get();
        }

        public Paciente Add(Paciente paciente)
        {
            return _studentRepository.Add(paciente);
        }

        public int Update(Paciente paciente)
        {
            return _studentRepository.Update(paciente);
        }

        public int Delete(Paciente paciente)
        {
            return _studentRepository.Delete(paciente);
        }

        public Paciente? GetById<Tid>(Tid id)
        {
            return _studentRepository.GetById(id);
        }
    }
}
