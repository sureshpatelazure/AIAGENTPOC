using MemoryVectorStorePOC.ChatCompletion;
using MemoryVectorStorePOC.TextSearch;
using MemoryVectorStorePOC.VectorStore;
using Microsoft.Extensions.AI;

namespace MemoryVectorStorePOC
{
    public static  class DemoApplication
    {
        public static  void Run()
        {
            // Initialize the Semantic Kernel with AI services
            var kernel = SemanticKernelCore.SemanticKernelService.CreateKernelWithAIService();
            IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator = Embedd.EmbeddGenerator.IEmbeddingGenerato(kernel);

            QdrantVectorStoreService qdrantVectorStoreService = new QdrantVectorStoreService(embeddingGenerator);

            string[] budgetInfo =
            {
                "The budget for 2020 is EUR 100 000",
                "The budget for 2021 is EUR 120 000",
                "The budget for 2022 is EUR 150 000",
                "The budget for 2023 is EUR 200 000",
                "The budget for 2024 is EUR 364 000"
            };

            string  query = "What is budget for 2020?"; 
            qdrantVectorStoreService.UpSert(budgetInfo).GetAwaiter().GetResult();
            //qdrantVectorStoreService.Search(query).GetAwaiter().GetResult();
            //TextSearchService.Search(query , qdrantVectorStoreService.Collection, embeddingGenerator).GetAwaiter().GetResult();
            ChatCompletionService.ChatWithAI(kernel, qdrantVectorStoreService.Collection, embeddingGenerator).GetAwaiter().GetResult(); 

        }
    }
}
