using Microsoft.SemanticKernel;
using ModelContextProtocol.Client;
using static Azure.Core.HttpHeader;

namespace MCPClient
{
    public static class SemanticKernelService
    {
        public static Kernel CreateKernel(IList<McpClientTool> tools, string ModelConnectorName)
        {
            IKernelBuilder kernelBuilder = Kernel.CreateBuilder();

            #pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            kernelBuilder.Plugins.AddFromFunctions("Tools", tools.Select(aifunction => aifunction.AsKernelFunction()));
            #pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            if (ModelConnectorName.ToLower() == "Ollama".ToLower())
                AddOllamaChatCompletion(kernelBuilder);

            return kernelBuilder.Build();
        }

        private static void AddOllamaChatCompletion(IKernelBuilder kernelBuilder)
        {
            #pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            kernelBuilder.Services.AddOllamaChatCompletion(Common.GetConfiguration("Ollama:OllamModelID"), new Uri(Common.GetConfiguration("Ollama:OllamUrl")));
            #pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        }
    }
}