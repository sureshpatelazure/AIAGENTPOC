using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using RAG_AIAgent_Qdrant_MCP_Demo.DataLoader;
using RAG_AIAgent_Qdrant_MCP_Demo.VectorStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAG_AIAgent_Qdrant_MCP_Demo.Embeding
{
    public class EmbeddingCore
    {

        private Kernel _kernel;
        private IEmbeddingGenerator<string, Embedding<float>> _embeddingGenerator;
        public EmbeddingCore(Kernel kernel)
        {
            _kernel = kernel;
            _embeddingGenerator = kernel.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>(); ;
        }

        public void UploadEmbedding(IConfiguration configuration)
        {
            var filePaths = configuration.GetSection("files:pdffiles:filepath").Get<string[]>();
            var batchSize = configuration.GetValue<int>("files:pdffiles:batchSize");

            var qdranturi = configuration.GetSection("vectorstore:qdrant:uri").Get<string>();
            var qdrantapikey = configuration.GetSection("vectorstore:qdrant:apikey").Get<string>();
            var collectionname = configuration.GetSection("files:pdffiles:collectionname").Get<string>();

           
            PDFLoader pDFLoader = new PDFLoader();
            var qdrantservice = new QdrantVectorStoreService(_embeddingGenerator, qdranturi, qdrantapikey, collectionname);

            foreach (var path in filePaths)
            {
                var pdfdata = pDFLoader.LoadPDF(path, batchSize).GetAwaiter().GetResult();
                foreach(var listtextsnippent in pdfdata)
                {
                    qdrantservice.UpSert(listtextsnippent).GetAwaiter().GetResult();
                }
                
            }


        }


    }
}
