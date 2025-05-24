using AIAgentLib.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Ollama;

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
    }
}