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

        SingleAIAgent singleAIAgent = new SingleAIAgent(aIConnectorService, configuration);
        await singleAIAgent.RunAgentForSingleConversation();
    }

    private static IConfiguration BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        return builder.Build();
    }
}   