namespace RetroGamesGo.Service.Helpers
{
    using System;
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationHelper
    {
        public static IConfigurationRoot Configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("local.settings.json", true, true)
            .AddEnvironmentVariables()
            .Build();
    }
}
