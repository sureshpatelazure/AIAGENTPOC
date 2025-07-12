using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using RAG_AIAgent_Qdrant_MCP_Demo.DataLoader;
using RAG_AIAgent_Qdrant_MCP_Demo.Embeding;
using RAG_AIAgent_Qdrant_MCP_Demo.SemanticKernel;
using RAG_AIAgent_Qdrant_MCP_Demo.VectorStore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAG_AIAgent_Qdrant_MCP_Demo
{
    public static class ApplicationDemo
    {
        public static void CreateAndStoreEmbedding(Kernel kernel ,IConfiguration configuration)
        {
           
            var qdranturi = configuration.GetSection("vectorstore:qdrant:uri").Get<string>();
            var qdrantapikey = configuration.GetSection("vectorstore:qdrant:apikey").Get<string>();
            var collectionname = configuration.GetSection("files:pdffiles:IndianBailJudgments:collectionname").Get<string>();

            IDataLoader pDFLoader = new PDFLoader();
            IEmbeddingGenerator<string, Embedding<float>> _embeddingGenerator = kernel.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>();
            IVectorStoreService vectorStoreService = new QdrantVectorStoreService(_embeddingGenerator, qdranturi , qdrantapikey , collectionname);

            var folderPath = configuration.GetSection("files:pdffiles:IndianBailJudgments:folderPath").Get<string>();
            var filePaths = configuration.GetSection("files:pdffiles:IndianBailJudgments:filepath").Get<string[]>();
            var batchSize = configuration.GetValue<int>("files:pdffiles:IndianBailJudgments:batchSize");
            var batchDivision = configuration.GetValue<int>("files:pdffiles:IndianBailJudgments:batchDivision");

            var folderFiles = Directory.GetFiles(folderPath);
            if(filePaths == null || filePaths.Length == 0)
            {
                filePaths = new string[0];
            }   
            filePaths = filePaths.Concat(folderFiles).ToArray();

            EmbeddingService embeddingService = new EmbeddingService(kernel, pDFLoader , vectorStoreService);
            embeddingService.UploadEmbedding(filePaths, batchDivision, batchSize);
        }

        public static Kernel CreateSemanticKernel(IConfiguration configuration)
        {
            var embeddingModel = configuration.GetSection("aiservice:embedding:ollama:model").Get<string>();
            var embeddingApiUrl = configuration.GetSection("aiservice:embedding:ollama:uri").Get<string>();

            var chatModel = configuration.GetSection("aiservice:chatcompletion:ollama:model").Get<string>();
            var chatApiUrl = configuration.GetSection("aiservice:chatcompletion:ollama:uri").Get<string>();

            SemanticKernelService semanticKernelService = new SemanticKernelService();
            return semanticKernelService.CreateKernelWithAIService(embeddingModel, embeddingApiUrl, chatModel, chatApiUrl);

        }
    }
}