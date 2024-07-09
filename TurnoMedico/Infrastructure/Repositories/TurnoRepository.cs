using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TurnoRepository : BaseRepository<Turno>, ITurnoRepository
    {
        public TurnoRepository(TurnoMedicoDbContext context) : base(context)
        {
        }

        // Métodos específicos de Turno se pueden añadir aquí si es necesario
    }

}
