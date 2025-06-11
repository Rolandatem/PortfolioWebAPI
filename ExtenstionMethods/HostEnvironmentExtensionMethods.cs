namespace PortfolioWebAPI;

public static class HostEnvironmentExtensionMethods
{
    public static bool IsDockerSolo(this IHostEnvironment hostEnvironment)
    {
        ArgumentNullException.ThrowIfNull(hostEnvironment);

        return hostEnvironment.IsEnvironment("DockerSolo");
    }

    public static bool IsDockerCompose(this IHostEnvironment hostEnvironment)
    {
        ArgumentNullException.ThrowIfNull(hostEnvironment);

        return hostEnvironment.IsEnvironment("DockerCompose");
    }
}