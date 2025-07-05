using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAG_AIAgent_Qdrant_MCP_Demo.SemanticKernelCore
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
