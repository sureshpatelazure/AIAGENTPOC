using AIAgentLib.Model;
using Microsoft.SemanticKernel;

namespace AIAgentLib.SemanticKernalService
{
    public class OllamaKernelChatCompletionService : IAIServiceConnector
    {
        public Kernel BuildChatCompletion<T>(T connectorConfiguration) where T : AIConnectorServiceConfiguration
        {
            var builder = CreateOllamaKernelBuilder(connectorConfiguration);
            return builder.Build();
        }

        public Kernel BuildChatCompletion<T>(T connectorConfiguration, List<object> Plugins) where T : AIConnectorServiceConfiguration
        {
            var builder = CreateOllamaKernelBuilder(connectorConfiguration);

            foreach (var plugin in Plugins)
            {
                builder.Plugins.AddFromObject(plugin);
            }

            return builder.Build();
        }

        private IKernelBuilder CreateOllamaKernelBuilder<T>(T connectorConfiguration) where T : AIConnectorServiceConfiguration
        {
            if (connectorConfiguration is OllamaConnectorServiceConfiguration ollama)
            {
                var builder = Kernel.CreateBuilder();
                builder.AddOllamaChatCompletion(ollama.ModelId, new Uri(ollama.Uri));
                if (ollama.useEmbeddingModel && !string.IsNullOrEmpty(ollama.EmbeddingModelId) && !string.IsNullOrEmpty(ollama.EmbeddingUrl))
                {
                    builder.AddOllamaEmbeddingGenerator(ollama.EmbeddingModelId, new Uri(ollama.EmbeddingUrl));
                }
                return builder;
            }
            else
            {
                throw new ArgumentException($"Unsupported model type: {typeof(T).Name}");
            }
        }
    }
}