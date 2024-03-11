using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace TestTask.Services.Authorization
{
    public class JwtAuthorizeAttribute : TypeFilterAttribute
    {
        public JwtAuthorizeAttribute() : base(typeof(JwtAuthorizeFilter))
        {
        }

        private class JwtAuthorizeFilter : IAuthorizationFilter
        {
     

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var token = GetTokenFromHeader(context.HttpContext.Request);
                if (string.IsNullOrEmpty(token))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(Config.SecretKey;);

                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    }, out _);
                }
                catch (Exception)
                {
                    context.Result = new UnauthorizedResult();
                }
            }

            private string GetTokenFromHeader(HttpRequest request)
            {
                var authorizationHeader = request.Headers["Authorization"].FirstOrDefault();
                if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                {
                    return authorizationHeader.Substring("Bearer ".Length).Trim();
                }
                return null;
            }
        }
    }
}
