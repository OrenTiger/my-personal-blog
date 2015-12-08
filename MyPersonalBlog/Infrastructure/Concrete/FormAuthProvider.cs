using System.Linq;
using System.Web.Security;
using MyPersonalBlog.Repositories;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Infrastructure
{
    public class FormAuthProvider : IAuthProvider
    {
        private IAdminRepository _repository;

        public FormAuthProvider(IAdminRepository adminRepository)
        {
            _repository = adminRepository;
        }

        public bool Authenticate(string login, string password)
        {
            Admin admin = _repository.Admins.Where(a => a.Login == login).FirstOrDefault();

            if (admin != null && Hashing.ValidatePassword(password, admin.PasswordHash))
            {
                FormsAuthentication.SetAuthCookie(login, true);
                return true;
            }
            else 
            {
                return false;
            }
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}