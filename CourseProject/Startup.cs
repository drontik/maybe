﻿using System.Web.Http;
using CourseProject.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Twitter;
using Microsoft.Owin.Security;
using System;

[assembly: OwinStartup(typeof(CourseProject.Startup))]

namespace CourseProject
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            var kernel = NinjectConfig.CreateKernel();

            var config = new HttpConfiguration();        

            WebApiConfig.Register(config);

            app.UseNinjectMiddleware(() => kernel)
                .UseOAuthAuthorizationServer(kernel.Get<MyOAuthAuthorizationServerOptions>().GetOptions())
                .UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions())
                .UseNinjectWebApi(config)
                .UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            app.UseTwitterAuthentication(new TwitterAuthenticationOptions
            {
                ConsumerKey = "iJVM5GqxMBBEyvz10Pxs3m6Lb",
                ConsumerSecret = "6KlIfTtKvpjetodFkd10uyNGZ4ghM8pIy6ywdTFAcqIWUgy6rE",
                BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(new[]
                {
                    "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2 
                    "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3 
                    "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5 
                    "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4 
                    "5168FF90AF0207753CCCD9656462A212B859723B", //DigiCert SHA2 High Assurance Server CA 
                    "B13EC36903F8BF4701D498261A0802EF63642BC3" //DigiCert High Assurance EV Root CA 
                })
            });

            // app.UseTwitterAuthentication(
            //   consumerKey: "iJVM5GqxMBBEyvz10Pxs3m6Lb",
            //   consumerSecret: "6KlIfTtKvpjetodFkd10uyNGZ4ghM8pIy6ywdTFAcqIWUgy6rE");

            app.UseFacebookAuthentication(
               appId: "1815580465355811",
               appSecret: "c04bfe4d9cc6b130553581b0f8f64ca6");

            app.UseVkontakteAuthentication("5676120", "Nu0vHe58WZdsBUv6aUr3", "{PERMISSIONS}");
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}
    
            