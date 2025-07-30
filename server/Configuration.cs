namespace Verbum.API;

static class Configuration {
    internal static string JWT_SECRET { get; } = Environment.GetEnvironmentVariable("JWT_SECRET");
    internal static string CLIENT_URL { get; } = Environment.GetEnvironmentVariable("CLIENT_URL");
}