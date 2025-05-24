using AIAgentLib.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAgentPOC
{
    public static class Common
    {
        public static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        public static OllamaConnectorServiceConfiguration GetOllamaConfiguration()
        {
            var config = BuildConfiguration();
            var ollamaSection = config.GetSection("Ollama");
            return new OllamaConnectorServiceConfiguration
            {
                ModelId = ollamaSection.GetValue<string>("ModelId"),
                Uri = ollamaSection.GetValue<string>("Url")
            };
        }
        public static string GetYamlContent(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The YAML file was not found at path '{filePath}'.");
            }
            return File.ReadAllText(filePath);
        }
    }
}
