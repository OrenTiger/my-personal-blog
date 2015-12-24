using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyPersonalBlog.Infrastructure;
using MyPersonalBlog.Infrastructure.OAuthProviders;
using MyPersonalBlog.Models;


namespace MyPersonalBlog.Controllers
{
    public class OAuthController : Controller
    {
        ISettingsProvider _settings;
        IOAuthProvider _oAuthProvider;

        private string _appId;
        private string _appSecret;
        private string _redirectUri;        

        public OAuthController (ISettingsProvider settings)
	    {
            _settings = settings;
	    }

        private IOAuthProvider getOAuthProvider(string provider)
        {
            _redirectUri = _settings.GetSettings().OAuthRedirectUri;

            switch (provider)
            {
                case "vk" : 
                        _appId = _settings.GetSettings().VkAppId;
                        _appSecret = _settings.GetSettings().VkAppSecret;
                        return new VkOAuthProvider(_appId, _appSecret, _redirectUri);                    
                case "google" :
                        _appId = _settings.GetSettings().GoogleAppId;
                        _appSecret = _settings.GetSettings().GoogleAppSecret;
                        return new GoogleOAuthProvider(_appId, _appSecret, _redirectUri);
                default:
                        return null;
            }
        }

        public RedirectResult SignIn(string code, string error, string returnToUrl, string provider)
        {            
            if (error != null)
            {
                return Redirect(Session["returnToUrl"].ToString());
            }
            else if (code == null)
            {
                _oAuthProvider = getOAuthProvider(provider);

                if (_oAuthProvider == null)
                {
                    return Redirect(returnToUrl);
                }

                Session.Add("returnToUrl", returnToUrl);
                Session.Add("provider", provider);

                return Redirect(_oAuthProvider.GetCodeUrl());
            }
            else
            {
                _oAuthProvider = getOAuthProvider(Session["provider"].ToString());

                if (_oAuthProvider == null)
                {
                    return Redirect(Session["returnToUrl"].ToString());
                }

                OAuthUser oAuthUser = _oAuthProvider.GetOAuthUser(code);
                Session.Add("oAuthUser", oAuthUser);

                return Redirect(Session["returnToUrl"].ToString());
            }
        }

        public RedirectResult SignOut()
        {            
            Session.Remove("oAuthUser");
            return Redirect(Session["returnToUrl"].ToString());
        }
    }
}