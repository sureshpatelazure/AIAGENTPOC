using MemoryVectorStorePOC.DataModel;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Microsoft.SemanticKernel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryVectorStorePOC.ChatCompletion
{
    public static class ChatCompletionService
    {

        public static async Task  ChatWithAI(Kernel kernel, QdrantCollection<ulong, FinanceInfo> collection, IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator)
        {
            #pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                var textSearch = new VectorStoreTextSearch<FinanceInfo>(collection, embeddingGenerator);
            #pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            var searchPlugin = textSearch.CreateWithGetTextSearchResults("SearchPlugin");

            kernel.Plugins.Add(searchPlugin);

            PromptExecutionSettings  promptExecutionSettings = new PromptExecutionSettings
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            };

            KernelArguments arguments = new(promptExecutionSettings );

            string query = string.Empty;

            while (query.ToLower() != "exit")
            {
                Console.WriteLine("Enter your query (or type 'exit' to quit): ");
                Console.WriteLine();
                query = Console.ReadLine();
                if (query == "exit")
                {
                    break;
                }
               
                var result = await kernel.InvokePromptAsync(query, arguments); 
                Console.WriteLine($"AI Response: {result}");
                Console.WriteLine();
            }   
         
        }
    }
}