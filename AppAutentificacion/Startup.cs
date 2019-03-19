using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using AppAutentificacion.Security;
using Microsoft.Owin.Extensions;
using System.Web.Http;

[assembly: OwinStartup(typeof(AppAutentificacion.Startup))]

namespace AppAutentificacion
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Para obtener más información sobre cómo configurar la aplicación, visite https://go.microsoft.com/fwlink/?LinkID=316888

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);  


            var myProvider = new MyAuthorizationServerProvider();

            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"), //Direccion donde se genera el token
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(20), //Validez token 
                Provider = myProvider,
            };


            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()).UseStageMarker(PipelineStage.Authenticate);
            app.UseOAuthAuthorizationServer(options);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);

        }
    }
}
