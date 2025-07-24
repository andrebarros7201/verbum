namespace Verbum.API;

static class Configuration {

    internal static string JwtKey { get; set; } = Environment.GetEnvironmentVariable("JWT_KEY");
}