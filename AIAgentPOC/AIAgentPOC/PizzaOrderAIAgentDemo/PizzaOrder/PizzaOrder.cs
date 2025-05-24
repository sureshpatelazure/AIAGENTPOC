using AIAgentLib;
using AIAgentLib.Model;
using AIAgentPOC.PizzaOrderAIAgentDemo.Plugin;

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

            List<Object> Plugins = new List<object>();
            Plugins.Add(new PizzaPlugin());

            _chatCompletionStartup = new ChatCompletionStartup(AIConnectorServiceType.Ollama, ollamaConfig, yamlContent, Plugins);
        }

        public async Task StartPizzaOrder()
        {
            Common.ChatWithAgent(_chatCompletionStartup).GetAwaiter().GetResult(); 

        }
    }
}
