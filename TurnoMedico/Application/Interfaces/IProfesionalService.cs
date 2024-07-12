using Application.Models.Dtos;
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
        IEnumerable<ProfesionalDto> GetAllProfesionales();
        ProfesionalDto GetProfesionalById(int id);
        void AddProfesional(ProfesionalDto profesionalDto);
        void UpdateProfesional(ProfesionalDto profesionalDto);
        void DeleteProfesional(int id);

    }

}
