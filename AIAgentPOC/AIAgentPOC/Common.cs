using AIAgentLib;
using AIAgentLib.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAgentPOC
{
    public static class Common
    {
        public static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        public static AIConnectorServiceConfiguration GetConnectorConfiguration(AIConnectorServiceType connectorType)
        {
            return connectorType switch
            {
                AIConnectorServiceType.Ollama => Common.GetOllamaConfiguration(),
                // Add more cases here for other connector types as needed
                _ => throw new InvalidOperationException($"Unsupported AI connector service type: {connectorType}")
            };
        }
        private static OllamaConnectorServiceConfiguration GetOllamaConfiguration()
        {
            var section = BuildConfiguration().GetSection("AIConnector:Ollama");
            if (!section.Exists())
                throw new InvalidOperationException("Missing configuration for AIConnector:Ollama");

            return new OllamaConnectorServiceConfiguration
            {
                ModelId = section.GetValue<string>("ModelId") ?? throw new InvalidOperationException("ModelId missing"),
                Uri = section.GetValue<string>("Url") ?? throw new InvalidOperationException("Url missing")
            };
        }

        public static DemoApplicationConfig GetDemoApplicationConfiguration(string appName)
        {
            var section = BuildConfiguration().GetSection("DemoApplication").GetSection(appName);
            if (!section.Exists())
                throw new InvalidOperationException($"Missing configuration for DemoApplication:{appName}");

            return new DemoApplicationConfig
            {
                AIConnectorName = section.GetValue<string>("AIConnectorName") ?? throw new InvalidOperationException("AIConnectorName missing"),
                YamlPromptFilePath = section.GetValue<string>("YamlPromptFilePath") ?? throw new InvalidOperationException("YamlPromptFilePath missing"),
                IsPluginPresent = bool.TryParse(section["IsPluginPresent"], out var present) ? present : throw new InvalidOperationException("IsPluginPresent invalid")
            };
        }

        public static string GetYamlContent(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The YAML file was not found at path '{filePath}'.");
            }
            return File.ReadAllText(filePath);
        }

        public static async Task ChatWithAgent(ChatCompletionStartup chatCompletionStartup)
        {
            Console.WriteLine();
            Console.Write("AI Agent> Please wait......" );
           
            // Start the chat with the AI agent by sending an initial message   
            var firstresponse = await chatCompletionStartup.ChatAIAgent("Please introduce yourself");
            Console.WriteLine();
            Console.Write(firstresponse);
            Console.WriteLine();

            bool isComplete = false;

            do
            {
                Console.WriteLine();
                Console.Write("User> ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                if (input.Trim().Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                {
                    isComplete = true;
                    break;
                }

                Console.WriteLine();
                Console.Write("AI Agent> Please wait......");
                Console.WriteLine();
                var response = await chatCompletionStartup.ChatAIAgent(input);
                Console.Write("AI Agent>  " + response);

                Console.WriteLine();

            } while (!isComplete);
        }
    }
}
