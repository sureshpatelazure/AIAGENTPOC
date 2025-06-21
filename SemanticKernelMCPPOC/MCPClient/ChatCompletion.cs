using Microsoft.SemanticKernel;

namespace MCPClient
{
    public static class ChatCompletion
    {
        public static async Task StartChatCompletion(Kernel kernel)
        {
            // Enable automatic function calling
            PromptExecutionSettings executionSettings = new()
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            };

            string prompt = "Who are you?";
            var result = await kernel.InvokePromptAsync(prompt, new(executionSettings));
            Console.WriteLine("AI Assistent:" + result);

            do
            {
                Console.WriteLine();
                Console.Write("User : ");
                prompt = Console.ReadLine()?? string.Empty;
                result = await kernel.InvokePromptAsync(prompt, new(executionSettings));
                Console.WriteLine("AI Assistent :" + result);

            } while (prompt.ToLower() != "exit");
        }
    }
}