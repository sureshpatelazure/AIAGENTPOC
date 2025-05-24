using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AIAgentLib.AIService
{
    public class ChatCompletionService : IChatCompletionService 
    {
        private readonly ChatHistoryAgentThread _chatHistoryAgentThread;
        private readonly ChatCompletionAgent _agent;

        public ChatCompletionService(ChatCompletionAgent agent)
        {
            _agent = agent ?? throw new ArgumentNullException(nameof(agent));
            _chatHistoryAgentThread = new ChatHistoryAgentThread();
        }

        public async Task<string?> ChatWithAIAgent(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return null;

            var message = new ChatMessageContent(AuthorRole.User, userInput);

            await foreach (ChatMessageContent response in _agent.InvokeAsync(message, _chatHistoryAgentThread))
            {
                return response.Content;
            }

            return null;
        }
    }
}
