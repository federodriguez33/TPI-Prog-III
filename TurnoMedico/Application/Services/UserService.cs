using Application.Interfaces;
using Application.Models.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)

        {
            _userRepository = userRepository;
        }

        public ICollection<UserDto> GetAll()
        {

            var users = _userRepository.GetAllUsers();
            return users.Select(u => new UserDto { Id = u.Id, Name = u.Name, Email = u.Email, Role = u.Role }).ToList();
        }

        public ICollection<UserDto> GetAllPacientes()
        {
            var clients = _userRepository.GetAllClients();
            return clients.Select(c => new UserDto { Id = c.Id, Name = c.Name, Email = c.Email, Role = c.Role }).ToList();
        }
        public ICollection<UserDto> GetAllProfesionales()
        {
            var clients = _userRepository.GetAllClients();
            return clients.Select(c => new UserDto { Id = c.Id, Name = c.Name, Email = c.Email, Role = c.Role }).ToList();
        }

        public ICollection<UserDto> GetAllAdmins()
        {
            var admins = _userRepository.GetAllAdmins();
            return admins.Select(a => new UserDto { Id = a.Id, Name = a.Name, Email = a.Email, Role = a.Role }).ToList();
        }

        public ICollection<UserDto> GetAllSuperAdmin()
        {
            var devs = _userRepository.GetAllDevs();
            return devs.Select(d => new UserDto { Id = d.Id, Name = d.Name, Email = d.Email, Role = d.Role }).ToList();
        }

        public User GetByName(string name)
        {
            var user = _userRepository.GetByName(name);
            return user;
        }

        public UserDto GetById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user != null)
            {
                return new UserDto { Name = user.Name, Email = user.Email, Role = user.Role };
            }
            else
            {
                throw new Exception("El usuario no existe");
            }
        }

        public UserDto Create(UserSaveRequest user)
        {
            switch (user.Role.ToLower())
            {
                case "admin":
                    var newAdmin = new Admin
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        Activo = true
                    };
                    _userRepository.AddUser(newAdmin);
                    return new UserDto { Name = newAdmin.Name, Email = newAdmin.Email, Role = newAdmin.Role };

                case "superadmin":
                    var newDev = new Dev
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        Activo = true
                    };
                    _userRepository.AddUser(newDev);
                    return new UserDto { Name = newDev.Name, Email = newDev.Email, Role = newDev.Role };

                case "paciente":
                    var newCliente = new Client
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        Activo = true
                    };
                    _userRepository.AddUser(newCliente);
                    return new UserDto { Name = newCliente.Name, Email = newCliente.Email, Role = newCliente.Role };

                case "profesional":
                    var newCliente = new Client
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        Activo = true
                    };
                    _userRepository.AddUser(newCliente);
                    return new UserDto { Name = newCliente.Name, Email = newCliente.Email, Role = newCliente.Role };

                default:
                    throw new ArgumentException("Error en Rol de User");
            }
        }

        public UserDto UpdateUser(int id, UserSaveRequest user)
        {
            var NotNullUser = _userRepository.GetUserById(id);
            if (NotNullUser == null)
            {
                throw new ArgumentException("El usuario no existe.");
            }

            NotNullUser.Name = user.Name;
            NotNullUser.Email = user.Email;

            _userRepository.UpdateUser(NotNullUser);
            return new UserDto { Name = NotNullUser.Name, Email = NotNullUser.Email, Role = NotNullUser.Role };
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user != null)
            {
                user.Activo = false;
                _userRepository.UpdateUser(user);
            }
            else
            {
                throw new Exception("El usuario no existe");
            }
        }

    }
}
