using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace RAG_AIAgent_Qdrant_MCP_Demo.SemanticKernel
{
    public class SemanticKernelService
    {
        public Kernel CreateKernelWithAIService(string embeddingModel, string embeddingApiUrl, string chatModel, string chatApiUrl)
        {
            var builder = Kernel.CreateBuilder();

            #pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            builder.AddOllamaChatCompletion(chatModel, new Uri(chatApiUrl));
            #pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                    
            #pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            builder.Services.AddOllamaEmbeddingGenerator(embeddingModel, new Uri(embeddingApiUrl));
            #pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            return builder.Build();
        }

    }
}
