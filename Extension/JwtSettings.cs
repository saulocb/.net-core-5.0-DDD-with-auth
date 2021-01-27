namespace Extensions
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Key { get; set; }
        public int ExpirationInDays { get; set; }
        public string Audience { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool ValidateLifetime { get; set; }
        public double ClockSkew { get; set; }
    }
}
