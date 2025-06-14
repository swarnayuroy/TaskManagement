using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace client.Utils
{
    public class JwtHelper
    {
        public static ClaimsPrincipal DecodeToken(string sessionToken)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(sessionToken);
            var identity = new ClaimsIdentity(token.Claims);
            return new ClaimsPrincipal(identity);
        }
    }
}