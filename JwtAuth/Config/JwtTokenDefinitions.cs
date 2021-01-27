using Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace JwtAuth.Config
{
    public static class JwtTokenDefinitions
    {
        public static void LoadFromConfiguration(IConfiguration configuration)
        {
            var config = configuration.GetSection("Jwt").Get<JwtSettings>();
            Key = config.Key;
            Audience = config.Audience;
            Issuer = config.Issuer;
            TokenExpirationTime = TimeSpan.FromMinutes(config.ExpirationInDays);
            ValidateIssuerSigningKey = config.ValidateIssuerSigningKey;
            ValidateLifetime = config.ValidateLifetime;
            ClockSkew = TimeSpan.FromMinutes(config.ClockSkew);
        }

        private static string _key = "";
        public static string Key
        {
            get => _key;
            set => _key = value.ToMD5();
        }

        public static SymmetricSecurityKey IssuerSigningKey
        {
            get => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        }

        public static SecurityKey IssuerSecurityKey
        {
            get => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        }

        public static TokenValidationParameters TokenValidationParameters
            => new TokenValidationParameters()
            {
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)),
                ClockSkew = TimeSpan.Zero
            };

        public static SigningCredentials SigningCredentials
        {
            get => new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha512);
        }

        public static EncryptingCredentials SigningCredentialsEncripted
        {
            get => new EncryptingCredentials(IssuerSigningKey, SecurityAlgorithms.Aes256KW, SecurityAlgorithms.Aes256CbcHmacSha512);
        }

        public static TimeSpan TokenExpirationTime { get; set; } = TimeSpan.FromHours(60);

        public static TimeSpan ClockSkew { get; set; } = TimeSpan.FromHours(24);

        public static string Issuer { get; set; } = "";

        public static string Audience { get; set; } = "";

        public static bool ValidateIssuerSigningKey { get; set; } = true;

        public static bool ValidateLifetime { get; set; } = true;

    }
}
