using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProfesionalService
    {
        public Profesional? GetByName(string name)
        {
            return _studentRepository.Get(name);
        }


        public List<Profesional> GetAll()
        {
            return _studentRepository.Get();
        }

        public Profesional Add(Profesional profesional)
        {
            return _studentRepository.Add(profesional);
        }

        public int Update(Profesional profesional)
        {
            return _studentRepository.Update(profesional);
        }

        public int Delete(Profesional profesional)
        {
            return _studentRepository.Delete(profesional);
        }

        public Profesional? GetById<Tid>(Tid id)
        {
            return _studentRepository.GetById(id);
        }
    }
}
