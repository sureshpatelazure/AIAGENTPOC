using Microsoft.Extensions.Configuration;

namespace MCPClient
{
    public static class Common
    {
        private static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        public static string  GetConfiguration(string configname)
        {
           return BuildConfiguration().GetValue<string>(configname);
        }
    }
}