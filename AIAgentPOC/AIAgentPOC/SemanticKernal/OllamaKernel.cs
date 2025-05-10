using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using static OllamaSharp.OllamaApiClient;

namespace AIAgentPOC.SemanticKernal
{
    public class OllamaKernel : IAIConnectorService
    {
        public Kernel BuildChatCompletionKernel(IConfiguration configuration)
        {
            var builder = Kernel.CreateBuilder();
            builder.AddOllamaChatCompletion(configuration.GetSection("Ollama")["ModelId"].ToString(), 
                new Uri(configuration.GetSection("Ollama")["Url"].ToString()));
            return builder.Build();
        }
    }
}
