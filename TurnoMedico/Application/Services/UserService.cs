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

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public void AddUser(User user)
        {
            // Verifica si el usuario ya existe por DNI y está activo
            var existingUser = _userRepository.FindActive(u => u.DNI == user.DNI).FirstOrDefault();

            if (existingUser != null)
            {
                throw new InvalidOperationException("El usuario ya existe.");
            }

            _userRepository.Add(user);
        }

        public void UpdateUser(User user)
        {
            // Verifica si otro usuario con el mismo DNI ya existe y está activo
            var existingUser = _userRepository.FindActive(u => u.DNI == user.DNI && u.Id != user.Id).FirstOrDefault();

            if (existingUser != null)
            {
                throw new InvalidOperationException("Ya existe otro usuario con el mismo DNI.");
            }

            _userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }

    }

}
