namespace PizzeriaAPI.Identity.JwtConfig
{
    public interface IJwtTokenConfig
    {
        string JwtIssuer { get; init; }
        string JwtAudience { get; init; }
        string JwtKey { get; init; }
        int TokenLifetimeInDays { get; init; }
    }
}
