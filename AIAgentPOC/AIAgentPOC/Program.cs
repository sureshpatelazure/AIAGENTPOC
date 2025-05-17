using AIAgentPOC.AIAgentPluginDemo.PizzaAIAgent;
using AIAgentPOC.PromptAIAgent;
using AIAgentPOC.SemanticKernal;
using AIAgentPOC.SingleAIAgent;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

internal class Program
{
    private static async Task Main(string[] args)
    {
        IAIConnectorService aIConnectorService = new OllamaKernel();
        IConfiguration configuration = BuildConfiguration();

        // SingleAIAgent singleAIAgent = new SingleAIAgent(aIConnectorService, configuration);
        // await singleAIAgent.RunAgentForSingleConversation();
        //await singleAIAgent.RunAgentForMultiPleConversation();

        // PromptAIAgent promptAIAgentprompt = new PromptAIAgent(aIConnectorService, configuration);
        // await promptAIAgentprompt.RunAgentForMultiPleConversation();

        PizzaAIAgent pizzaAIAgent = new PizzaAIAgent(aIConnectorService, configuration);
       await pizzaAIAgent.StartPizzaOrder();

    }

    private static IConfiguration BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        return builder.Build();
    }
}   