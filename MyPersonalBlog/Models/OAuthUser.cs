namespace MyPersonalBlog.Models
{
    public class OAuthUser
    {
        public string AccessToken { get; set; }
        public string ProfilePhoto { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Provider { get; set; }
    }
}