using AIAgentLib.Model;
using Microsoft.Extensions.Configuration;

namespace CommonLib
{
    public class Configuration
    {
        IConfiguration _configuration = null!;  

        public Configuration(string AppSettingPath)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppSettingPath, optional: false, reloadOnChange: true);

                _configuration =  builder.Build();
        }

        public  AIConnectorServiceConfiguration GetConnectorConfiguration(AIConnectorServiceType connectorType)
        {
            return connectorType switch
            {
                AIConnectorServiceType.Ollama => GetOllamaConfiguration(),
                // Add more cases here for other connector types as needed
                _ => throw new InvalidOperationException($"Unsupported AI connector service type: {connectorType}")
            };
        }
        private  OllamaConnectorServiceConfiguration GetOllamaConfiguration()
        {
            var section = _configuration.GetSection("AIConnector:Ollama");
            if (!section.Exists())
                throw new InvalidOperationException("Missing configuration for AIConnector:Ollama");

            return new OllamaConnectorServiceConfiguration
            {
                ModelId = section.GetValue<string>("ModelId") ?? throw new InvalidOperationException("ModelId missing"),
                Uri = section.GetValue<string>("Url") ?? throw new InvalidOperationException("Url missing"),
                useEmbeddingModel = section.GetValue<bool>("useEmbeddingModel"),
                EmbeddingModelId = section.GetValue<string>("EmbeddingModelId") ?? throw new InvalidOperationException("EmbeddingModelId missing"),
                EmbeddingUrl = section.GetValue<string>("EmbeddingUrl") ?? throw new InvalidOperationException("EmbeddingUrl missing")
            };
        }

        public  string GetYamlContent(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The YAML file was not found at path '{filePath}'.");
            }
            return File.ReadAllText(filePath);
        }
    }
}
