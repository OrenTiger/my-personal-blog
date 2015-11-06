namespace MyPersonalBlog.Infrastructure
{
    public interface IAuthProvider
    {
        bool Authenticate(string login, string password);
        void SignOut();
    }
}
