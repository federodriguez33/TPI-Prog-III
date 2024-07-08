using Domain.Interfaces;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        
        private readonly TurnoMedicoDbContext _context;

        public UserRepository(TurnoMedicoDbContext context)
        {
            _context = context;
        }

    }
}
