using Bcrypt = BCrypt.Net.BCrypt;

namespace MyPersonalBlog.Infrastructure
{
    public class HashingProvider : IHashingProvider
    {
        private string GetRandomSalt()
        {
            return Bcrypt.GenerateSalt(12);
        }

        public string HashPassword(string password)
        {
            return Bcrypt.HashPassword(password, GetRandomSalt());
        }

        public bool ValidatePassword(string password, string correctHash)
        {
            return Bcrypt.Verify(password, correctHash);
        }
    }
}