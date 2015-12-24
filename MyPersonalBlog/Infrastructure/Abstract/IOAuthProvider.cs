using MyPersonalBlog.Models;

namespace MyPersonalBlog.Infrastructure.OAuthProviders
{
    public interface IOAuthProvider
    {
        string GetCodeUrl();
        OAuthUser GetOAuthUser(string code);
    }
}
