using AIAgentLib.AIAgent;
using AIAgentLib.AIService;
using AIAgentLib.Model;
using AIAgentLib.RAGAIService;
using AIAgentLib.SemanticKernalService;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Data;
using System.Linq.Expressions;

namespace AIAgentLib
{
    public class ChatCompletionStartup
    {
        IChatCompletionService _chatCompletionService;

        private void InitializeChatCompletionService(
           AIConnectorServiceType aIConnectorServiceType,
           AIConnectorServiceConfiguration aIConnectorServiceConfiguration,
           string yamlContent,
           List<object>? plugins, EmbeddingConfiguration embeddingConfiguration)
        {
            Kernel kernel = plugins == null
                ? CreateKernel(aIConnectorServiceType, aIConnectorServiceConfiguration)
                : CreateKernel(aIConnectorServiceType, aIConnectorServiceConfiguration, plugins);

            ChatCompletionAgent agent = CreateAgent(kernel, yamlContent);
            if (embeddingConfiguration != null)
            {
                RAGService rAGService = new RAGService();
                var textSearchProvider = rAGService.AddDocumentAsync(kernel, embeddingConfiguration).GetAwaiter().GetResult();
                _chatCompletionService = new ChatCompletionService(agent, textSearchProvider);
            }
            else
            {
                _chatCompletionService = new ChatCompletionService(agent);
            }
        }

        public ChatCompletionStartup(
                AIConnectorServiceType aIConnectorServiceType,
                AIConnectorServiceConfiguration aIConnectorServiceConfiguration,
                string yamlContent)
        {
            InitializeChatCompletionService(
                aIConnectorServiceType,
                aIConnectorServiceConfiguration,
                yamlContent,
                null,null);
        }

        public ChatCompletionStartup(
            AIConnectorServiceType aIConnectorServiceType,
            AIConnectorServiceConfiguration aIConnectorServiceConfiguration,
            string yamlContent,
            List<object> Plugins, EmbeddingConfiguration embeddingConfiguration)
        {
            InitializeChatCompletionService(
                aIConnectorServiceType,
                aIConnectorServiceConfiguration,
                yamlContent,
                Plugins, embeddingConfiguration);
        }


        private IAIServiceConnector GetAIServiceConnector(AIConnectorServiceType aIConnectorServiceType)
        {
            return aIConnectorServiceType switch
            {
                AIConnectorServiceType.Ollama => new OllamaKernelChatCompletionService(),
                _ => throw new ArgumentException($"Unsupported AI connector service type: {aIConnectorServiceType}")
            };
        }

        private Kernel CreateKernel(AIConnectorServiceType aIConnectorServiceType, AIConnectorServiceConfiguration aIConnectorServiceConfiguration)
        {
            IAIServiceConnector aIConnectorService = GetAIServiceConnector(aIConnectorServiceType);
            return aIConnectorService.BuildChatCompletion(aIConnectorServiceConfiguration);
        }

        private Kernel CreateKernel(AIConnectorServiceType aIConnectorServiceType, AIConnectorServiceConfiguration aIConnectorServiceConfiguration, List<object> Plugins)
        {
            IAIServiceConnector aIConnectorService = GetAIServiceConnector(aIConnectorServiceType);
            return aIConnectorService.BuildChatCompletion(aIConnectorServiceConfiguration, Plugins);
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

 
