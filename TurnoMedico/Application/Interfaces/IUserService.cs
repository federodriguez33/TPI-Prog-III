using Application.Models.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        ICollection<UserDto> GetAll();
        ICollection<UserDto> GetAllPacientes();
        ICollection<UserDto> GetAllProfesionales();
        ICollection<UserDto> GetAllAdmins();
        ICollection<UserDto> GetAllSuperAdmin();
        User GetByName(string name);
        UserDto GetById(int id);
        UserDto Create(UserSaveRequest user);
        UserDto UpdateUser(int id, UserSaveRequest user);
        void DeleteUser(int id);

    }
}
