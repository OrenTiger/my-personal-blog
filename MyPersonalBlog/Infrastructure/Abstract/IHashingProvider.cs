namespace MyPersonalBlog.Infrastructure
{
    public interface IHashingProvider
    {
        string HashPassword(string password);

        bool ValidatePassword(string password, string correctHash);
    }
}
