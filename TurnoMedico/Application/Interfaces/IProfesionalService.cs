using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProfesionalService
    {
        IEnumerable<Profesional> GetAllProfesionales();
        Profesional GetProfesionalById(int id);
        void AddProfesional(Profesional profesional);
        void UpdateProfesional(Profesional profesional);
        void DeleteProfesional(int id);

    }

}
