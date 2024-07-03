using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TurnoService
    {
        public Turno? GetByFechaHora(string fechahora)
        {
            return _studentRepository.Get(fechahora);
        }

        public List<Turno> GetAll()
        {
            return _studentRepository.Get();
        }

        public Turno Add(Turno turno)
        {
            return _studentRepository.Add(turno);
        }

        public int Update(Turno turno)
        {
            return _studentRepository.Update(turno);
        }

        public int Delete(Turno turno)
        {
            return _studentRepository.Delete(turno);
        }

        public Turno? GetById<Tid>(Tid id)
        {
            return _studentRepository.GetById(id);
        }
    }
}
