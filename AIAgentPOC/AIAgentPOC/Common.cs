using AIAgentLib;
using Microsoft.Extensions.Configuration;

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

        public static DemoApplicationConfig GetDemoApplicationConfiguration(string appName)
        {
            var section = BuildConfiguration().GetSection("DemoApplication").GetSection(appName);
            if (!section.Exists())
                throw new InvalidOperationException($"Missing configuration for DemoApplication:{appName}");

            return new DemoApplicationConfig
            {
                AIConnectorName = section.GetValue<string>("AIConnectorName") ?? throw new InvalidOperationException("AIConnectorName missing"),
                YamlPromptFilePath = section.GetValue<string>("YamlPromptFilePath") ?? throw new InvalidOperationException("YamlPromptFilePath missing"),
                IsPluginPresent = bool.TryParse(section["IsPluginPresent"], out var present) ? present : throw new InvalidOperationException("IsPluginPresent invalid"),
                IsRAGEnabled = bool.TryParse(section["IsRAGEnabled"], out var isragenabled) ? isragenabled : throw new InvalidOperationException("IsRAGEnabled invalid"),
                EmbeddingCollectionName = section.GetValue<string>("EmbeddingCollectionName") ?? throw new InvalidOperationException("EmbeddingCollectionName missing"),
                EmbeddingDocumentContent = section.GetValue<string>("EmbeddingDocumentContent") ?? throw new InvalidOperationException("EmbeddingDocumentContent missing"),
            };
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
                var response = await chatCompletionStartup.ChatAIAgent(input);
                Console.Write("AI Agent>  " + response);

                Console.WriteLine();

            } while (!isComplete);
        }
    }
}
