﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VoiceAcademyAPILogin.DTOs;

namespace VoiceAcademyAPILogin.Utility
{
    public class TokenGenerator
    {
        private static IConfigurationRoot configuration;

        private static void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();
        }

        public static JsonResult GetToken(TokenDTO newToken)
        {
            if (configuration == null)
            {
                InitializeConfiguration();
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Key")));

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("id", newToken.idUser.ToString()),
            new Claim("roleType", newToken.Role),
        };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );
            newToken.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return new JsonResult(
                new
                {
                    correct = true,
                    StatusCode = 200,
                    message = "",
                    token = newToken,
                });
        }
    }
}
