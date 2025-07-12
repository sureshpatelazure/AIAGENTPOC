using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Qdrant.Client;

namespace RAG_AIAgent_Qdrant_MCP_Demo.VectorStore
{
    public class QdrantVectorStoreService : IVectorStoreService
    {
        private readonly QdrantVectorStore? _vectorStore;
        private readonly QdrantCollection<ulong, DataLoader.DataContent>? _collection;
        public QdrantVectorStoreService(IEmbeddingGenerator embeddingGenerator, string uri, string apikey, string collectionname)
        {
            QdrantClient qdrantClient = new QdrantClient(new Uri(uri), apikey);

            _vectorStore = new QdrantVectorStore(qdrantClient, true,
               new QdrantVectorStoreOptions
               {
                   EmbeddingGenerator = embeddingGenerator,
               });
            _collection = _vectorStore.GetCollection<ulong, DataLoader.DataContent>(collectionname);

            _collection.EnsureCollectionExistsAsync().GetAwaiter().GetResult();
        }

        public async Task UpSert(IEnumerable<DataLoader.DataContent> dataContents)
        {
            var data = dataContents.Select(snippet => new DataLoader.DataContent
            {
                Key = snippet.Key,
                Text = snippet.Text, // You can modify the text as needed
            });

            await _collection.UpsertAsync(data);
        }
        public QdrantCollection<ulong, DataLoader.DataContent>? Collection => _collection;
    }
}