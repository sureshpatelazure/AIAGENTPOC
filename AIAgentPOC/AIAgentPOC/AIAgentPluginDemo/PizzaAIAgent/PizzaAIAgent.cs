using AIAgentPOC.Model;
using AIAgentPOC.SemanticKernal;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAgentPOC.AIAgentPluginDemo.PizzaAIAgent
{
    public class PizzaAIAgent
    {
        IConfiguration _configuration;
        IAIConnectorService _aIConnectorService;
        Kernel _Kernel;

        public PizzaAIAgent (IAIConnectorService aIConnectorService, IConfiguration configuration)
        {
            _configuration = configuration;
            _aIConnectorService = aIConnectorService;
            _Kernel = CreateKernel();
        }

        private Kernel CreateKernel()
        {
            return _aIConnectorService.BuildChatCompletionKernel(_configuration);
        }

        private AIAgentInput GetAgentProfile()
        {
            AIAgentInput aIAgentInput = new AIAgentInput();
            aIAgentInput.Instructions = """
                You are a friendly assistant who will take pizza order from user. 
                Ask user to select pizza, toppings and size beforing adding to cart.
                User can order mulitple pizza.
                Display selected cart before proceding order.
                Ask user for adding/removing pizza from cart before ordering.
                Require user approval for completing order
                If the user doesn't provide enough information for you to complete a order, you will keep asking questions until you have
                enough information to complete the order.
                Display sentence on new line.
                """;

            aIAgentInput.Name = "Pizza Order AI Agent ";
            aIAgentInput.Kernel = _Kernel;

            return aIAgentInput;
        }

        private ChatCompletionAgent CreateAgent()
        {
            AIAgentInput agentInput = GetAgentProfile();
            ChatCompletionAgent agent = new()
            {
                Instructions = agentInput.Instructions,
                Name = agentInput.Name,
                Kernel = agentInput.Kernel,
            };

            return agent;
        }

        public async Task StartPizzaOrder()
        {
            ChatCompletionAgent agent = CreateAgent();

            ChatHistoryAgentThread chatHistoryAgentThread = new ChatHistoryAgentThread();

            await IntroduceAIAgent(agent, chatHistoryAgentThread);

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
                Console.WriteLine("Assistant> Thinking.......");
                Console.WriteLine();

                await foreach (StreamingChatMessageContent response in agent.InvokeStreamingAsync(message, chatHistoryAgentThread))
                {
                    Console.Write(response.Content);
                }

                Console.WriteLine();

            } while (!isComplete);

            await chatHistoryAgentThread.DeleteAsync();
        }
       
        private async Task IntroduceAIAgent(ChatCompletionAgent agent, ChatHistoryAgentThread chatHistoryAgentThread) {

            string input = "Who are you?";
            var message = new ChatMessageContent(AuthorRole.User, input);

            Console.WriteLine();
            Console.WriteLine("Assistant>");
            Console.WriteLine();

            await foreach (StreamingChatMessageContent response in agent.InvokeStreamingAsync(message, chatHistoryAgentThread))
            {
                Console.Write(response.Content);
            }

            Console.WriteLine();
        }
    }
}
