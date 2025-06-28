using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;


namespace MemoryVectorStorePOC.SemanticKernelCore
{
    public static class SemanticKernelService
    {
        public static Kernel CreateKernelWithAIService()
        {
           
            var builder = Kernel.CreateBuilder();
            #pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            builder.AddOllamaChatCompletion("llama3.2:latest", new Uri("http://localhost:11434"));
            #pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            #pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            builder.Services.AddOllamaEmbeddingGenerator("granite-embedding:30m", new Uri("http://localhost:11434"));
            #pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            
           
            return builder.Build();
        }


    }
}
