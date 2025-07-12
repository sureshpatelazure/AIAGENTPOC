using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Data;
using RAG_AIAgent_Qdrant_MCP_Demo.VectorStore;
using YamlDotNet.Serialization;

namespace RAG_AIAgent_Qdrant_MCP_Demo.AIAgent
{
    public class AIAgent
    {
        private readonly Kernel _kernel;
        private ChatCompletionAgent _chatCompletionAgent;
        private ChatHistoryAgentThread _chatHistoryAgentThread;
        private readonly QdrantVectorStoreService _vectorStoreService;
        private readonly IEmbeddingGenerator<string, Embedding<float>> _embeddingGenerator;
        private readonly string _yamlfilePath;

        public AIAgent(Kernel kernel, string yamlfilePath, QdrantVectorStoreService vectorStoreService)
        {
            _kernel = kernel;
            _chatHistoryAgentThread = new ChatHistoryAgentThread();
            _chatCompletionAgent = new ChatCompletionAgent();
            _vectorStoreService = vectorStoreService;
            _yamlfilePath = yamlfilePath;
            _embeddingGenerator = _kernel.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>(); ;
        }
        public void CreateAIAgent()
        {
            var yamlContent = ReadYamlContent(_yamlfilePath);
            var yamlData = ReadYaml(yamlContent);

            PromptTemplateConfig templateConfig = new(yamlContent);
            KernelPromptTemplateFactory templateFactory = new KernelPromptTemplateFactory();

            #pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            var textSearch = new VectorStoreTextSearch<DataLoader.DataContent>(_vectorStoreService.Collection, _embeddingGenerator);
            #pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            var searchPlugin = textSearch.CreateWithGetTextSearchResults("SearchPlugin");
            _kernel.Plugins.Add(searchPlugin);

            _chatCompletionAgent = new(templateConfig, templateFactory)
            {
                Kernel = _kernel,
                Arguments = new KernelArguments(
                     new PromptExecutionSettings
                     {
                         FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                     }
                ),
                Description = (string)yamlData["description"]
            };
        }

        public async Task ChatWithAgntInConsole()
        {
            if (_chatCompletionAgent == null)
            {
                throw new InvalidOperationException("AIAgent is not initialized. Call CreateAIAgent first.");
            }
            string userInput = string.Empty;

            while (!userInput.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Enter your query (or type 'exit' to quit): ");
                Console.WriteLine();
                userInput = Console.ReadLine();
                if (userInput == "exit")
                {
                    break;
                }

                var message = new ChatMessageContent(AuthorRole.User, userInput);
                var result = string.Empty;
                await foreach (ChatMessageContent response in _chatCompletionAgent.InvokeAsync(message, _chatHistoryAgentThread))
                {
                    result = response.Content;
                }

                Console.WriteLine($"AI Response: {result}");
                Console.WriteLine();
            }
        }

        private string ReadYamlContent(string filePath)
        {
            return File.ReadAllText(filePath);
        }
        private Dictionary<string, object> ReadYaml(string yamlContent)
        {
            var deserializer = new DeserializerBuilder().Build();
            var result = deserializer.Deserialize<Dictionary<string, object>>(yamlContent);
            return result;
        }
    }
}