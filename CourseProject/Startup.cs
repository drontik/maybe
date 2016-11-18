using System.Web.Http;
using CourseProject.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

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
        }
    }
}
    
            