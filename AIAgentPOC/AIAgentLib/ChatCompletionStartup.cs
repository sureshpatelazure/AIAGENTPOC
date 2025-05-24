using AIAgentLib.AIAgent;
using AIAgentLib.AIService;
using AIAgentLib.Model;
using AIAgentLib.SemanticKernalService;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAgentLib
{
    public class ChatCompletionStartup
    {
        IChatCompletionService _chatCompletionService;

        public ChatCompletionStartup(AIConnectorServiceType aIConnectorServiceType,
            AIConnectorServiceConfiguration aIConnectorServiceConfiguration,
            string yamlContent)
        {
            Kernel kernel = CreateKernel(aIConnectorServiceType, aIConnectorServiceConfiguration);
            ChatCompletionAgent agent = CreateAgent(kernel, yamlContent);
            _chatCompletionService = new ChatCompletionService(agent);
        }
        private Kernel CreateKernel(AIConnectorServiceType aIConnectorServiceType, AIConnectorServiceConfiguration aIConnectorServiceConfiguration)
        {
            IAIServiceConnector aIConnectorService = aIConnectorServiceType switch
            {
                AIConnectorServiceType.Ollama => new OllamaKernelChatCompletionService(),
                _ => throw new ArgumentException($"Unsupported AI connector service type: {aIConnectorServiceType}")
            };

            return aIConnectorService.BuildChatCompletion(aIConnectorServiceConfiguration);

        }

        private ChatCompletionAgent CreateAgent(Kernel kernel, string yamlContent)
        {
            IAIAgent aIAgent = new AIAgent.AIAgent();
            return aIAgent.CreateAIAgent(kernel, new KernelArguments(), yamlContent);
        }

        public async Task<string?> ChatAIAgent(string userInput)
        {
            if (_chatCompletionService == null)
                throw new InvalidOperationException("ChatCompletionService is not initialized.");
            return await _chatCompletionService.ChatWithAIAgent(userInput);

        }
    }
}

 
