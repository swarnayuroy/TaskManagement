using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace microservice.Utils.AuthenticationService
{
    public class Authentication
    {
        public static string GenerateToken(Models.User userDetail)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userDetail.Id.ToString()),
                    new Claim(ClaimTypes.Name, userDetail.Name),
                    new Claim(ClaimTypes.Email, userDetail.Email)
                };
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["config:JwtKey"]));
                var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                    (
                        issuer: Convert.ToString(ConfigurationManager.AppSettings["config:JwtIssuer"]),
                        audience: Convert.ToString(ConfigurationManager.AppSettings["config:JwtAudience"]),
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception)
            {
                throw new AuthenticationException("Failed to generate token");
            }            
        }
        public static bool ValidateToken(string token)
        {
            try
            {
                if (!String.IsNullOrEmpty(token))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityKey = Encoding.ASCII.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["config:JwtKey"]));

                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                        ValidateIssuer = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var userId = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;
                    var userName = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;

                    if (!String.IsNullOrEmpty(userId) && !String.IsNullOrEmpty(userName))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw new AuthenticationException("Failed to validate token");
            }            
        }
    }
}