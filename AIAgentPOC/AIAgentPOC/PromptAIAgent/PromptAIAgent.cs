using AIAgentPOC.Model;
using AIAgentPOC.SemanticKernal;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAgentPOC.PromptAIAgent
{
    public class PromptAIAgent
    {
        IConfiguration _configuration;
        IAIConnectorService _aIConnectorService;
        Kernel _Kernel;

        public PromptAIAgent(IAIConnectorService aIConnectorService, IConfiguration configuration)
        {
            _configuration = configuration;
            _aIConnectorService = aIConnectorService;
            _Kernel = CreateKernel();
        }

        public async Task RunAgentForMultiPleConversation()
        {
            ChatCompletionAgent agent = CreateAgent("GenerateStory.yml");
            ChatHistoryAgentThread chatHistoryAgentThread = new ChatHistoryAgentThread();

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

                var message = new ChatMessageContent(AuthorRole.User, input);

                Console.WriteLine();
                Console.WriteLine("AI Agent> Thinking.......");

                await foreach (ChatMessageContent response in agent.InvokeAsync(message, chatHistoryAgentThread))
                {
                    Console.WriteLine();
                    Console.WriteLine(response.Content);
                }


            } while (!isComplete);
        }

        private Kernel CreateKernel()
        {
            return _aIConnectorService.BuildChatCompletionKernel(_configuration);
        }

        private ChatCompletionAgent CreateAgent(string Name)
        {
            string generateStoryYaml = File.ReadAllText("./Prompts/" + Name);
            PromptTemplateConfig templateConfig = new PromptTemplateConfig(generateStoryYaml);
            KernelPromptTemplateFactory templateFactory = new KernelPromptTemplateFactory();

            ChatCompletionAgent agent = new(templateConfig, templateFactory)
            {
                Kernel = _Kernel,
                Arguments =
                     {
                         { "topic", "Dog" },
                         { "GenerateStory.yml", "3" },
                     }
            };

            return agent;
        }

    }
}