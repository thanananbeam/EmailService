
using EmailService.DataAccess.Attributes;
using EmailService.Model.UserEmail;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace EmailService.DataAccess
{
    public static class JWT
    {
        private static string secret = "aaaaaaaaaaaaaaaMe0ooww-!!31!!22#*?";
        public static string EnvName;

        public static string GenerateToken(string email)
        {
            byte[] key = Encoding.ASCII.GetBytes(JWT.secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

            int timeout = Startup.StaticConfig.GetValue<int>("JwtEmail:timeout");

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email, email),
                }),
                Expires = DateTime.UtcNow.AddMinutes(timeout),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);

            return handler.WriteToken(token);
        }

        public static UserEmailModel ValidateToken(HttpContext context, AuthorizationType type = AuthorizationType.None)
        {
            if (!context.Request.Headers.Keys.Contains("Authorization"))
            {
                string nameCode = nameof(HttpStatusCode.RequestTimeout);
                throw new SecurityTokenException(nameCode);
            }
            
            string token = context.Request.Headers["Authorization"];
            token = token.Replace("Bearer ", "");

            return DecryptionToken(token, type);
        }

        public static UserEmailModel ValidateToken(string token, AuthorizationType type = AuthorizationType.None)
        {
            return DecryptionToken(token, type);
        }

        private static UserEmailModel DecryptionToken(string token, AuthorizationType type)
        {
            UserEmailModel usermodel = new UserEmailModel();

            try
            {

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                {
                    throw new SecurityTokenException("Invalid Authorization");
                }

                byte[] key = Encoding.ASCII.GetBytes(JWT.secret);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                SecurityToken securityToken;

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                if (principal == null)
                {
                    throw new SecurityTokenException("Invalid ClaimsPrincipal");
                }

                var exp = DateTimeOffset.FromUnixTimeSeconds(long.Parse(principal.FindFirst("exp").Value));
                var timeexpire = exp.LocalDateTime;
                if (DateTime.Now >= timeexpire)
                {
                    string nameCode = nameof(HttpStatusCode.RequestTimeout);
                    throw new SecurityTokenException(nameCode);
                }


            }
            catch (Exception ex)
            {

                throw new SecurityTokenException(ex.ToString());
            }

            return usermodel;
        }

        public static class Iconfig
        {
            private static IConfiguration config;
            public static IConfiguration Configuration
            {
                get
                {
                    if (JWT.EnvName == "Development")
                    {
                        var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.Development.json");
                        config = builder.Build();
                        return config;
                    }
                    else
                    {
                        var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");
                        config = builder.Build();
                        return config;
                    }

                }
            }
        }
    }
}
