using System.Collections.Generic;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public interface IAdminRepository
    {
        IEnumerable<Admin> Admins { get; }

        void SaveAdmin(Admin admin);

        Admin DeleteAdmin(int adminId);
    }
}
