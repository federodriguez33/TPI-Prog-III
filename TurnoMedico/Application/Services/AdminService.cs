using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public void CreateAdmin(Admin admin)
        {
            _adminRepository.Add(admin);
        }

        public void UpdateAdmin(Admin admin)
        {
            _adminRepository.Update(admin);
        }

        public void DeleteAdmin(int adminId)
        {
            _adminRepository.Delete(adminId);
        }

    }

}
