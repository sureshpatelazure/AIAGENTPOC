using AIAgentLib.Model;
using AIAgentLib.MultiAgent;
using AIAgentPOC.AgentOrchestration.Model;
using CommonLib;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.Agents;

namespace AIAgentPOC.AgentOrchestration
{
    public static class DemoAgentOrchestration
    {
        private static AgentOrchestrationConfig ReadFromAppSettings(string OrchestrationPatternsName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var sectionPath = $"DemoAgentOrchestration:{OrchestrationPatternsName}";
            return configuration.GetSection(sectionPath).Get<AgentOrchestrationConfig>();
        }

        public static void RunOrchestration(AgentOrchestrationPattern OrchestrationPatterns,string userInput, List<object> Plugins = null)
        {

            Configuration configuration = new Configuration("appsettings.json");

            if (string.IsNullOrWhiteSpace(OrchestrationPatterns.ToString()))
                throw new ArgumentException("Orchestration Patterns Name cannot be null or empty.");

            // Get Demo Application Configuration
            var demoAgentOrchestrationConfig = ReadFromAppSettings(OrchestrationPatterns.ToString());

            // Get AI Connector Configuration   
            var connectorType = Enum.Parse<AIConnectorServiceType>(demoAgentOrchestrationConfig.AIConnectorName, ignoreCase: true);
            var connectorConfig = configuration.GetConnectorConfiguration(connectorType);

            List<string> yamlContents = new List<string>();

            foreach (var agent in demoAgentOrchestrationConfig.Agent)
            {
                if (string.IsNullOrWhiteSpace(agent.Name) || string.IsNullOrWhiteSpace(agent.YamlPromptFilePath))
                    throw new InvalidOperationException("Agent Name and YamlPromptFilePath cannot be null or empty.");

                // Get Prompt Yaml File
                var yamlContent = configuration.GetYamlContent(agent.YamlPromptFilePath);
                if (string.IsNullOrWhiteSpace(yamlContent))
                    throw new InvalidOperationException($"YAML content is empty or could not be loaded from path: {agent.YamlPromptFilePath}");

                // Plugin validation
                if (demoAgentOrchestrationConfig.IsPluginPresent == "true")
                {
                    if (Plugins is null || Plugins.Count == 0)
                        throw new InvalidOperationException("Plugins cannot be null or empty when IsPluginPresent is true.");
                }
                else if (Plugins is { Count: > 0 })
                {
                    throw new InvalidOperationException("Plugins should be null or empty when IsPluginPresent is false.");
                }

                yamlContents.Add(yamlContent);
            }

            AIAgents aIAgents = new AIAgents(
                connectorType,
                connectorConfig,
                Plugins);

           var result = aIAgents.RunOrchestration(OrchestrationPatterns, yamlContents,userInput);
           Console.WriteLine($"# RESULT:\n{string.Join("\n\n", result.Select(text => $"{text}"))}");
        }
    }
}


