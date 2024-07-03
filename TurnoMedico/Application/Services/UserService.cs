using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService
    {
        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetByName(string name)
        {
            return _userRepository.Get(name);
        }

        public int Add(User user)
        {

            return _userRepository.Add(user);

        }

    }
}
