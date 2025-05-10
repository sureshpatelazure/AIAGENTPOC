using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIAgentPOC.SemanticKernal;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AIAgentPOC.SingleAIAgent
{
    public class SingleAIAgent
    {
        IAIConnectorService _aIConnectorService;
        Kernel _Kernel;

        public SingleAIAgent(IAIConnectorService aIConnectorService)
        {
            _aIConnectorService = aIConnectorService;
            _Kernel = CreateKernel();
        }
        public async Task RunAgentForSingleConversation()
        {

            ChatCompletionAgent agent = CreateAgent();


            ChatHistory chat = new ChatHistory();
            chat.Add(new ChatMessageContent(AuthorRole.User, "What is the difference between a class and a record?"));

            ChatHistoryAgentThread chatHistoryAgentThread = new ChatHistoryAgentThread();

            await foreach (ChatMessageContent response in agent.InvokeAsync(chat, chatHistoryAgentThread))
            {
                chat.Add(response);
                Console.WriteLine(response.Content);
            }
        }

        private Kernel CreateKernel()
        {
            return _aIConnectorService.BuildChatCompletionKernel();
        }

        private ChatCompletionAgent CreateAgent()
        {
            ChatCompletionAgent agent = new()
            {
                Instructions = "Answer questions about c# and .net",
                Name = "C# Agent",
                Kernel = _Kernel
            };

            return agent;
        }
    }
}
