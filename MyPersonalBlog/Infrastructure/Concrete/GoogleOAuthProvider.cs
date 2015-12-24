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
    public class GoogleOAuthProvider : IOAuthProvider
    {
        private string _appId;
        private string _appSecret;
        private string _redirectUri;

        public GoogleOAuthProvider(string appId, string appSecret, string redirectUri)
        {
            _appId = appId;
            _appSecret = appSecret;
            _redirectUri = redirectUri;
        }

        public string GetCodeUrl()
        {
            string authUrl = "https://accounts.google.com/o/oauth2/auth";
            string scope = "https://www.googleapis.com/auth/userinfo.profile";

            var address = String.Format("{0}?client_id={1}&redirect_uri={2}&response_type=code&scope={3}", authUrl, _appId, _redirectUri, scope);
            return address;
        }

        class AccessToken
        {
            public string access_token = null;
            public string token_type = null;
            public string expires_in = null;
            public string user_id = null;
        }

        class UserData
        {
            public string id = null;
            public string email = null;
            public string name = null;
            public string picture = null;
        }

        public OAuthUser GetOAuthUser(string code)
        {
            string tokenUrl = "https://accounts.google.com/o/oauth2/token";
            string userInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

            var parameters = String.Format("code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type=authorization_code", 
                code, _appId, _appSecret, _redirectUri);

            var response = GoogleOAuthProvider.LoadPost(tokenUrl, parameters);
            var accessToken = GoogleOAuthProvider.DeserializeJson<AccessToken>(response);

            var address = String.Format("{0}?access_token={1}", userInfoUrl, accessToken.access_token);
            response = GoogleOAuthProvider.Load(address);

            var userData = GoogleOAuthProvider.DeserializeJson<UserData>(response);

            return new OAuthUser
            {
                Name = userData.name,
                UserId = userData.id,
                AccessToken = accessToken.access_token,
                ProfilePhoto = userData.picture,
                Provider = "google"
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

        private static string LoadPost(string url, string parameters)
        {
            var request = WebRequest.Create(url) as HttpWebRequest;

            var postData = parameters;
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

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