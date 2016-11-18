using Microsoft.Owin.Security.OAuth;

namespace CourseProject.Interfaces
{
    public interface IOAuthAuthorizationServerOptions
    {
        OAuthAuthorizationServerOptions GetOptions();
    };
}