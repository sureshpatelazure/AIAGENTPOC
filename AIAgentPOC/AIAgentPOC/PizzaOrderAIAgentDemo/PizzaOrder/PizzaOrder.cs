using AIAgentLib;
using AIAgentLib.Model;

namespace AIAgentPOC.PizzaOrderAIAgentDemo.PizzaOrder
{
    public class PizzaOrder
    {
        private ChatCompletionStartup _chatCompletionStartup;
        public PizzaOrder()
        {
            // Initialize the AI agent with the model ID and YAML content
            var ollamaConfig = Common.GetOllamaConfiguration();
            string yamlContent = Common.GetYamlContent("C:\\GenAI\\GitHub Project\\AIAgentPOC\\AIAgentPOC\\PizzaOrderAIAgentDemo\\Prompt\\PizzaOrder.yaml");
            _chatCompletionStartup = new ChatCompletionStartup(AIConnectorServiceType.Ollama, ollamaConfig, yamlContent);
        }

        public async Task StartPizzaOrder()
        {

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

                var response = await _chatCompletionStartup.ChatAIAgent(input); 
                Console.Write(response);
                

                Console.WriteLine();

            } while (!isComplete);

        }
    }
}
