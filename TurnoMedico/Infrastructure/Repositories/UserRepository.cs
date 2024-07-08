using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TurnoMedicoDbContext context) : base(context)
        {
        }

        // Aquí puedes añadir métodos específicos de User, si es necesario
    }
}