using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bcrypt = BCrypt.Net.BCrypt;

namespace MyPersonalBlog.Infrastructure
{
    public class Hashing
    {
        private static string GetRandomSalt()
        {
            return Bcrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password)
        {
            return Bcrypt.HashPassword(password, GetRandomSalt());
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return Bcrypt.Verify(password, correctHash);
        }
    }
}