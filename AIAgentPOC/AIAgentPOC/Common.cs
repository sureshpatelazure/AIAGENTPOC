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

        public static OllamaConnectorServiceConfiguration GetOllamaConfiguration()
        {
            var config = BuildConfiguration();
            var ollamaSection = config.GetSection("Ollama");
            return new OllamaConnectorServiceConfiguration
            {
                ModelId = ollamaSection.GetValue<string>("ModelId"),
                Uri = ollamaSection.GetValue<string>("Url")
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
                var response = await chatCompletionStartup.ChatAIAgent(input);
                Console.Write("AI Agent>  " + response);

                Console.WriteLine();

            } while (!isComplete);
        }
    }
}
