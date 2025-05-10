using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AIAgentPOC.SemanticKernal
{
    public class OllamaKernel : IAIConnectorService
    {
        public Kernel BuildChatCompletionKernel()
        {
            var builder = Kernel.CreateBuilder();
            builder.AddOllamaChatCompletion("gemma3:1b", new Uri("http://localhost:11434"));
            return builder.Build();
        }
    }
}
