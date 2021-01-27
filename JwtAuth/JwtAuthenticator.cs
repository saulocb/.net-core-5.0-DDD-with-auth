using Bouncer.Common.Exceptions;
using Bouncer.ViewModels.AppObject;
using Bouncer.ViewModels.AppObjects;
using JwtAuth.Config;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace JwtAuth
{
    public static class JwtAuthenticator
    {
        public static TokenResult GenerateToken(UserApp_vw user, IList<string> roles)
        {
            if (user == null)
                throw new InvalidLoginException();

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Active", user.Active.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var expires = DateTime.Now.AddDays(1);

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Issuer = JwtTokenDefinitions.Issuer,
                Audience = JwtTokenDefinitions.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                NotBefore = DateTime.Now,
                SigningCredentials = JwtTokenDefinitions.SigningCredentials,
            });

            return new TokenResult()
            {
                Value = handler.WriteToken(securityToken),
                User = user,
                Expires = expires
            };
        }

        public static UserApp_vw GetUser(string token)
        {
            var principal = GetPrincipal(token);

            return new UserApp_vw()
            {
                Id = Convert.ToInt64(ExtractClaimValueFromPrinciple(principal, ClaimTypes.NameIdentifier)),
                Email = ExtractClaimValueFromPrinciple(principal, ClaimTypes.Name),
                Role = ExtractClaimValueFromPrinciple(principal, ClaimTypes.Role),
                UserName = ExtractClaimValueFromPrinciple(principal, ClaimTypes.Name),
                Active = Convert.ToBoolean(ExtractClaimValueFromPrinciple(principal, "Active"))
            };
        }

        private static string ExtractClaimValueFromPrinciple(ClaimsPrincipal claimsPrinciple, string type)
        {
            try
            {
                var a = claimsPrinciple.Claims.Where(c => c.Type == type);

                return a.Select(c => c.Value).FirstOrDefault().ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                token = token.Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                    return null;

                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, JwtTokenDefinitions.TokenValidationParameters, out securityToken);

                return principal;
            }
            catch (Exception )
            {
                return null;
            }
        }
    }
}
