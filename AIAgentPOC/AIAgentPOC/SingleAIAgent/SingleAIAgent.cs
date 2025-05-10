using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIAgentPOC.Model;
using AIAgentPOC.SemanticKernal;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.ChatCompletion;
using static OllamaSharp.OllamaApiClient;

namespace AIAgentPOC.SingleAIAgent
{
    public class SingleAIAgent
    {
        IConfiguration _configuration;
        IAIConnectorService _aIConnectorService;
        Kernel _Kernel;

        public SingleAIAgent(IAIConnectorService aIConnectorService, IConfiguration configuration)
        {
            _configuration = configuration;
            _aIConnectorService = aIConnectorService;
            _Kernel = CreateKernel();
        }
        public async Task RunAgentForSingleConversation()
        {

            ChatCompletionAgent agent = CreateAgent(GetAIAgentInput("Agent"));

            ChatHistory chat = new ChatHistory();
            chat.Add(new ChatMessageContent(AuthorRole.User, "What is the difference between a class and a structure in 10-15 sentence?"));

            ChatHistoryAgentThread chatHistoryAgentThread = new ChatHistoryAgentThread();

            await foreach (ChatMessageContent response in agent.InvokeAsync(chat, chatHistoryAgentThread))
            {
                chat.Add(response);
                Console.WriteLine(response.Content);
            }
        }
        private Kernel CreateKernel()
        {
            return _aIConnectorService.BuildChatCompletionKernel(_configuration);
        }

        private ChatCompletionAgent CreateAgent(AIAgentInput agentInput)
        {
            ChatCompletionAgent agent = new()
            {
                Instructions = agentInput.Instructions,
                Name = agentInput.Name,
                Kernel = agentInput.Kernel
            };

            return agent;
        }

        private AIAgentInput GetAIAgentInput(string Name)
        {
            AIAgentInput aIAgentInput = new AIAgentInput();
            aIAgentInput.Instructions = "Answer questions about C#";
            aIAgentInput.Name = "C# Agent";
            aIAgentInput.Kernel = _Kernel;

            return aIAgentInput;
        }
    }
}
