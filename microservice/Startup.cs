using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Owin;
using System.Configuration;

[assembly: OwinStartup(typeof(microservice.Startup))]
namespace microservice
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Convert.ToString(ConfigurationManager.AppSettings["config:JwtIssuer"]),
                    ValidAudience = Convert.ToString(ConfigurationManager.AppSettings["config:JwtAudience"]),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["config:JwtKey"]))
                }
            });
        }
    }
}