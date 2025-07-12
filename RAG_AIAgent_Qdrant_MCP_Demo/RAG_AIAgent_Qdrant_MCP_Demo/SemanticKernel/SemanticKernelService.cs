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
            // builder.AddOllamaChatCompletion(chatModel, new Uri(chatApiUrl));
            builder.AddHuggingFaceChatCompletion("google/gemma-2-2b-it", new Uri("https://router.huggingface.co/nebius/"), "1");
            #pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            #pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            builder.Services.AddOllamaEmbeddingGenerator(embeddingModel, new Uri(embeddingApiUrl));
            //builder.Services.AddHuggingFaceEmbeddingGenerator("mixedbread-ai/mxbai-embed-large-v1", new Uri("https://router.huggingface.co/hf-inference/models/mixedbread-ai/mxbai-embed-large-v1/pipeline/feature-extraction/"), "");     
            #pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            return builder.Build();
        }

    }
}
