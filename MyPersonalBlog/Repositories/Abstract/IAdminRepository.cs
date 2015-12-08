using System.Collections.Generic;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public interface IAdminRepository
    {
        IEnumerable<Admin> Admins { get; }

        void Save(Admin admin);

        Admin Delete(int adminId);
    }
}
