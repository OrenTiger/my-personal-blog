using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using MyPersonalBlog.Models;
using MyPersonalBlog.Repositories;
using System.Text;

namespace MyPersonalBlog.Infrastructure.OAuthProviders
{
    public class VkOAuthProvider : IOAuthProvider
    {
        private string _appId;
        private string _appSecret;
        private string _redirectUri;

        public VkOAuthProvider(string appId, string appSecret, string redirectUri)
        {
            _appId = appId;
            _appSecret = appSecret;
            _redirectUri = redirectUri;
        }

        public string GetCodeUrl()
        {
            var address = String.Format("https://oauth.vk.com/authorize?client_id={0}&redirect_uri={1}&response_type=code&scope=email&display=page", _appId, _redirectUri);
            return address;
        }

        class AccessToken
        {
            public string access_token = null;
            public string user_id = null;
        }

        class UserData
        {
            public string uid = null;
            public string first_name = null;
            public string last_name = null;
            public string photo_50 = null;
        }

        class UsersData
        {
            public UserData[] response = null;
        }

        public OAuthUser GetOAuthUser(string code)
        {
            var address = String.Format("https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&code={2}&redirect_uri={3}",
                _appId, _appSecret, code, _redirectUri);

            var response = VkOAuthProvider.Load(address);
            var accessToken = VkOAuthProvider.DeserializeJson<AccessToken>(response);

            address = String.Format("https://api.vk.com/method/users.get?uids={0}&fields=photo_50", accessToken.user_id);

            response = VkOAuthProvider.Load(address);
            var usersData = VkOAuthProvider.DeserializeJson<UsersData>(response);
            var userData = usersData.response.First();

            return new  OAuthUser {
                Name = userData.first_name + " " + userData.last_name,
                UserId = accessToken.user_id,
                AccessToken = accessToken.access_token,
                ProfilePhoto = userData.photo_50,
                Provider = "vk"
            };
        }

        private static string Load(string address)
        {
            var request = WebRequest.Create(address) as HttpWebRequest;

            using (var response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static T DeserializeJson<T>(string input)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(input);
        }
    }
}