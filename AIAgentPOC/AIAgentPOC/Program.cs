using AIAgentPOC.SemanticKernal;
using AIAgentPOC.SingleAIAgent;

internal class Program
{
    private static async Task Main(string[] args)
    {
        IAIConnectorService aIConnectorService = new OllamaKernel();
        SingleAIAgent singleAIAgent = new SingleAIAgent(aIConnectorService);
        await singleAIAgent.RunAgentForSingleConversation();
    }
}   