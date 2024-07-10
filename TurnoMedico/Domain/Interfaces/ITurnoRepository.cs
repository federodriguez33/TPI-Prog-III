using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITurnoRepository
    {
        IEnumerable<Turno> GetAll();
        Turno GetById(int id);
        void Add(Turno turno);
        void Update(Turno turno);
        void Delete(int id);
        bool IsTurnoAvailable(Turno turno);
    }

}
