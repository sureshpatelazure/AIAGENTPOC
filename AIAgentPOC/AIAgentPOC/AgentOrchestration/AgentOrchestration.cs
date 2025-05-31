using AIAgentLib.Model;
using AIAgentLib.MultiAgent;
using AIAgentPOC.AgentOrchestration.Model;
using CommonLib;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.Agents;

namespace AIAgentPOC.AgentOrchestration
{
    public class AgentOrchestration
    {
        private AgentOrchestrationConfig ReadFromAppSettings(string OrchestrationPatternsName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var sectionPath = $"DemoAgentOrchestration:{OrchestrationPatternsName}";
            return configuration.GetSection(sectionPath).Get<AgentOrchestrationConfig>();
        }

        private List<ChatCompletionAgent> CreateAgents(AgentOrchestrationPattern OrchestrationPatterns, List<object> Plugins = null)
        {

            Configuration configuration = new Configuration("appsettings.json");

            if (string.IsNullOrWhiteSpace(OrchestrationPatterns.ToString()))
                throw new ArgumentException("Orchestration Patterns Name cannot be null or empty.");

            // Get Demo Application Configuration
            var demoAgentOrchestrationConfig = ReadFromAppSettings(OrchestrationPatterns.ToString());

            // Get AI Connector Configuration   
            var connectorType = Enum.Parse<AIConnectorServiceType>(demoAgentOrchestrationConfig.AIConnectorName, ignoreCase: true);
            var connectorConfig = configuration.GetConnectorConfiguration(connectorType);

            AIAgents aIAgents = new AIAgents(connectorType, connectorConfig, Plugins);
            List<ChatCompletionAgent> agents = new List<ChatCompletionAgent>();

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

                agents.Add(aIAgents.CreateAgent(yamlContent));
            }

            return agents;
        }

        public async Task Run(AgentOrchestrationPattern OrchestrationPatterns, List<object> Plugins = null)
        {
            try
            {
                var agents = CreateAgents(OrchestrationPatterns, Plugins);
                if (agents.Count == 0)
                    throw new InvalidOperationException("No agents were created. Please check your configuration.");
                
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error in Run: {ex.Message}");
                throw;
            }
        }
    }
}


