using System.Collections.Generic;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private BlogContext _db = new BlogContext();

        public IEnumerable<Admin> Admins
        {
            get
            {
                return _db.Admins;
            }
        }

        public void SaveAdmin(Admin admin)
        {
            if (admin.Id == 0)
            {
                _db.Admins.Add(admin);
            }
            else
            {
                Admin _admin = _db.Admins.Find(admin.Id);

                if (_admin != null)
                {
                    _admin.Login = admin.Login;
                    _admin.Username = admin.Username;
                    _admin.PasswordHash = admin.PasswordHash;
                }
            }

            _db.SaveChanges();
        }

        public Admin DeleteAdmin(int adminId)
        {
            Admin _admin = _db.Admins.Find(adminId);

            if (_admin != null)
            {
                _db.Admins.Remove(_admin);
                _db.SaveChanges();
            }

            return null;
        }
    }
}