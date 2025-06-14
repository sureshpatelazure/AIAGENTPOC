using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Data;

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
    
        #pragma warning disable SKEXP0130 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        public ChatCompletionService(ChatCompletionAgent agent, TextSearchProvider textSearchProvider)
        #pragma warning restore SKEXP0130 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        {
            _agent = agent ?? throw new ArgumentNullException(nameof(agent));
            _chatHistoryAgentThread = new ChatHistoryAgentThread();

            #pragma warning disable SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            _chatHistoryAgentThread.AIContextProviders.Add(textSearchProvider);
            #pragma warning restore SKEXP0110 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
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
