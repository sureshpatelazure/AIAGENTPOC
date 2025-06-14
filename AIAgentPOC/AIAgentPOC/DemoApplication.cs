using AIAgentLib;
using AIAgentLib.Model;
using CommonLib;

namespace AIAgentPOC
{
    public class DemoApplication
    {
        public static async Task Run(string demoApplicationName, List<object> Plugins = null)
        {
            try
            {
             
                 Configuration configuration = new Configuration("appsettings.json");  

                if (string.IsNullOrWhiteSpace(demoApplicationName))
                    throw new ArgumentException("Demo application name cannot be null or empty.", nameof(demoApplicationName));

                // Get Demo Application Configuration
                var demoConfig = Common.GetDemoApplicationConfiguration(demoApplicationName);

                // Get AI Connector Configuration   
                var connectorType = Enum.Parse<AIConnectorServiceType>(demoConfig.AIConnectorName, ignoreCase: true);
                var connectorConfig = configuration.GetConnectorConfiguration(connectorType);

                // Get Prompt Yaml File
                var yamlContent = configuration.GetYamlContent(demoConfig.YamlPromptFilePath);

                if (string.IsNullOrWhiteSpace(yamlContent))
                    throw new InvalidOperationException($"YAML content is empty or could not be loaded from path: {demoConfig.YamlPromptFilePath}");

                // Plugin validation
                if (demoConfig.IsPluginPresent)
                {
                    if (Plugins is null || Plugins.Count == 0)
                        throw new InvalidOperationException("Plugins cannot be null or empty when IsPluginPresent is true.");
                }
                else if (Plugins is { Count: > 0 })
                {
                    throw new InvalidOperationException("Plugins should be null or empty when IsPluginPresent is false.");
                }

                // RAG validation
                EmbeddingConfiguration embeddingConfiguration = null;
                if (demoConfig.IsRAGEnabled && string.IsNullOrWhiteSpace(demoConfig.EmbeddingCollectionName))
                {
                    throw new InvalidOperationException("EmbeddingCollectionName cannot be null or empty when IsRAGEnabled is true.");
                } 
                else if (demoConfig.IsRAGEnabled && string.IsNullOrWhiteSpace(demoConfig.EmbeddingDocumentContent))
                {
                    throw new InvalidOperationException("EmbeddingDocumentContent cannot be null or empty when IsRAGEnabled is true.");
                }
                else if (demoConfig.IsRAGEnabled && 
                         (!string.IsNullOrWhiteSpace(demoConfig.EmbeddingCollectionName) || 
                          !string.IsNullOrWhiteSpace(demoConfig.EmbeddingDocumentContent)))
                {
                    embeddingConfiguration = new EmbeddingConfiguration
                    {
                        CollectionName = demoConfig.EmbeddingCollectionName,
                        DocumentContent = demoConfig.EmbeddingDocumentContent
                    };  
                }


                var chatStartup = new ChatCompletionStartup(connectorType, connectorConfig, yamlContent, Plugins, embeddingConfiguration);
                await Common.ChatWithAgent(chatStartup);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error in Run: {ex.Message}");
                throw;
            }
        }

    }
    public class DemoApplicationConfig
    {
        public string AIConnectorName { get; set; }

        public string YamlPromptFilePath { get; set; }
        public bool IsPluginPresent { get; set; }
        public bool IsRAGEnabled { get; set; }
        public string EmbeddingCollectionName { get; set; }
        public string EmbeddingDocumentContent { get; set; }

    }
}
