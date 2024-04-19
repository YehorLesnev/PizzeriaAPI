namespace PizzeriaAPI.Identity.JwtConfig
{
    public class JwtTokenConfig : IJwtTokenConfig
    {
        public string JwtIssuer { get; init; }
        public string JwtAudience { get; init; }
        public string JwtKey { get; init; }
        public int TokenLifetimeInDays { get; init; }
    }
}
