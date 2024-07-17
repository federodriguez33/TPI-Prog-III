using Application.Models.Dtos;
using Application.Models.Request;
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
        void AddProfesional(ProfesionalSaveRequest profesionalSaveRequest);
        void UpdateProfesional(ProfesionalSaveRequest profesionalSaveRequest);
        void DeleteProfesional(int id);

    }

}
