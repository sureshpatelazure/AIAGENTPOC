using AIAgentLib.Model;
using Microsoft.SemanticKernel;

namespace AIAgentLib.SemanticKernalService
{
    public class OllamaKernelChatCompletionService : IAIServiceConnector
    {
        public Kernel BuildChatCompletion<T>(T connectorConfiguration) where T : AIConnectorServiceConfiguration
        {
            if (connectorConfiguration is OllamaConnectorServiceConfiguration ollama)
            {
                var builder = Kernel.CreateBuilder();
                builder.AddOllamaChatCompletion(ollama.ModelId, new Uri(ollama.Uri));
                return builder.Build();
            }
           else
            {
                throw new ArgumentException($"Unsupported model type: {typeof(T).Name}");
            }
        }

        public Kernel BuildChatCompletion<T>(T connectorConfiguration, List<object> Plugins) where T : AIConnectorServiceConfiguration
        {
            if (connectorConfiguration is OllamaConnectorServiceConfiguration ollama)
            {
                var builder = Kernel.CreateBuilder();
                builder.AddOllamaChatCompletion(ollama.ModelId, new Uri(ollama.Uri));

                foreach (var plugin in Plugins)
                {
                    builder.Plugins.AddFromObject(plugin);
                }

                return builder.Build();
            }
            else
            {
                throw new ArgumentException($"Unsupported model type: {typeof(T).Name}");
            }
        }
    }
}