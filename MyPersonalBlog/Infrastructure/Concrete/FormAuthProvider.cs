using System.Linq;
using System.Web.Security;
using MyPersonalBlog.Repositories;
using MyPersonalBlog.Models;

namespace MyPersonalBlog.Infrastructure
{
    public class FormAuthProvider : IAuthProvider
    {
        private IAdminRepository _repository;
        private IHashingProvider _hashingProvider;

        public FormAuthProvider(IAdminRepository adminRepository, IHashingProvider hashingProvider)
        {
            _repository = adminRepository;
            _hashingProvider = hashingProvider;
        }

        public bool Authenticate(string login, string password)
        {
            Admin admin = _repository.Admins.Where(a => a.Login == login).FirstOrDefault();

            if (admin != null && _hashingProvider.ValidatePassword(password, admin.PasswordHash))
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