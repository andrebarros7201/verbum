namespace Verbum.API;

static class Configuration {
    internal static string JWT_SECRET { get; } = Environment.GetEnvironmentVariable("JWT_SECRET");
}