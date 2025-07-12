using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace RAG_AIAgent_Qdrant_MCP_Demo.AIAgent
{
    public class AIAgent
    {
        private ChatCompletionAgent _chatCompletionAgent;
        private ChatHistoryAgentThread _chatHistoryAgentThread;
        public void CreateAIAgent(Kernel kernel,string yamlfilePath)
        {
            _chatHistoryAgentThread = new ChatHistoryAgentThread();

            var yamlContent = ReadYamlContent(yamlfilePath);
            var yamlData = ReadYaml(yamlContent);

            PromptTemplateConfig templateConfig = new PromptTemplateConfig(yamlContent);
            KernelPromptTemplateFactory templateFactory = new KernelPromptTemplateFactory();

            _chatCompletionAgent = new(templateConfig, templateFactory)
            {
                Kernel = kernel,
                Arguments = new KernelArguments(
                     new PromptExecutionSettings
                     {
                         FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                     }
                ),
                Description = (string)yamlData["description"]
            };
        }

        public async Task ChatWithAgntInConsole(Kernel kernel)
        {
            if (_chatCompletionAgent == null)
            {
                throw new InvalidOperationException("AIAgent is not initialized. Call CreateAIAgent first.");
            }
            string userInput = string.Empty;

            while (userInput.ToLower() != "exit")
            {
                Console.WriteLine("Enter your query (or type 'exit' to quit): ");
                Console.WriteLine();
                userInput = Console.ReadLine();
                if (userInput == "exit")
                {
                    break;
                }

                var message = new ChatMessageContent(AuthorRole.User, userInput);
                var result = string.Empty;
                await foreach (ChatMessageContent response in _chatCompletionAgent.InvokeAsync(message, _chatHistoryAgentThread))
                {
                    result = response.Content;

                }
                
                Console.WriteLine($"AI Response: {result}");
                Console.WriteLine();
            }

        }

        private  string ReadYamlContent(string filePath)
        {
           return File.ReadAllText(filePath);
        }

        private  Dictionary<string, object> ReadYaml(string yamlContent)
        {
            var deserializer = new DeserializerBuilder().Build();
            var result = deserializer.Deserialize<Dictionary<string, object>>(yamlContent);
            return result;
        }


    }
}
