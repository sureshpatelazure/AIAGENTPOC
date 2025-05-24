namespace AIAgentLib.AIService
{
    public interface IChatCompletionService
    {
        public Task<string?> ChatWithAIAgent(string userInput);


    }
}
