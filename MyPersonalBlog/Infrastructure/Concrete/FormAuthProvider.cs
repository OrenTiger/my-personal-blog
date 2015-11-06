using System.Linq;
using System.Web.Security;
using MyPersonalBlog.Repositories;

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
            if (_repository.Admins.FirstOrDefault(a => a.Login == login && a.PasswordHash == password) != null)
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