using System;
using CourseProject.Interfaces;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;

namespace CourseProject.Providers
{
    public class MyOAuthAuthorizationServerOptions : IOAuthAuthorizationServerOptions
    {
        private readonly SimpleAuthorizationServerProvider provider;

        public MyOAuthAuthorizationServerOptions(SimpleAuthorizationServerProvider provider)
        {
            this.provider = provider;
        }

        public OAuthAuthorizationServerOptions GetOptions()
        {
            return new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,

                TokenEndpointPath = new PathString("/token"),

                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),

                Provider = provider
            };
        }
    }
}