using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        private readonly TurnoMedicoDbContext _context;

        public UserRepository(TurnoMedicoDbContext context) : base(context)
        {
            _context = context;
        }

        public Paciente GetPacienteByDNI(string DNI)
        {
            return _context.Pacientes.SingleOrDefault(u => u.DNI == DNI);
        }

        public Profesional GetProfesionalByDNI(string DNI)
        {
            return _context.Profesionales.SingleOrDefault(u => u.DNI == DNI);
        }
    }
}