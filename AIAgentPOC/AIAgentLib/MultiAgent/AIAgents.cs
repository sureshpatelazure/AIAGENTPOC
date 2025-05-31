using AIAgentLib.AIAgent;
using AIAgentLib.Model;
using AIAgentLib.SemanticKernalService;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;

namespace AIAgentLib.MultiAgent
{
    public class AIAgents
    {

        Kernel _kernel;
        private void InitializeKernel(
           AIConnectorServiceType aIConnectorServiceType,
           AIConnectorServiceConfiguration aIConnectorServiceConfiguration,
           List<object>? plugins)
        {
            _kernel = plugins == null
                ? CreateKernel(aIConnectorServiceType, aIConnectorServiceConfiguration)
                : CreateKernel(aIConnectorServiceType, aIConnectorServiceConfiguration, plugins);
        }

        public AIAgents(
                AIConnectorServiceType aIConnectorServiceType,
                AIConnectorServiceConfiguration aIConnectorServiceConfiguration,
                string yamlContent)
        {
            InitializeKernel(
                aIConnectorServiceType,
                aIConnectorServiceConfiguration,
                null);
        }

        public AIAgents(
            AIConnectorServiceType aIConnectorServiceType,
            AIConnectorServiceConfiguration aIConnectorServiceConfiguration,
            List<object> Plugins)
        {
            InitializeKernel(
                aIConnectorServiceType,
                aIConnectorServiceConfiguration,
                Plugins);
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
        private ChatCompletionAgent CreateAgent(string yamlContent)
        {
            IAIAgent aIAgent = new AIAgent.AIAgent();
            return aIAgent.CreateAIAgent(_kernel, new KernelArguments(), yamlContent);
        }

        public void RunOrchestration(AgentOrchestrationPattern agentOrchestrationPattern, List<string> yamlContent)
        {
            List<ChatCompletionAgent> agents = new List<ChatCompletionAgent>(); 

            foreach(var yaml in yamlContent)
            {
                if (string.IsNullOrWhiteSpace(yaml))
                    throw new InvalidOperationException("YAML content cannot be null or empty.");
                var agent = CreateAgent(yaml);
                agents.Add(agent);
            }   
        }
    }
}