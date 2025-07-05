using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Qdrant.Client;

namespace RAG_AIAgent_Qdrant_MCP_Demo.VectorStore
{
    public class QdrantVectorStoreService<T> where T : class, IVectorRecord, new()
    {
        private QdrantVectorStore? _vectorStore;
        private QdrantCollection<ulong, T>? _collection;

        public QdrantVectorStoreService(IEmbeddingGenerator embeddingGenerator, string uri, string apikey, string collectionname)
        {
            QdrantClient qdrantClient = new QdrantClient(new Uri(uri), apikey);

            _vectorStore = new QdrantVectorStore(qdrantClient, true,
               new QdrantVectorStoreOptions
               {
                   EmbeddingGenerator = embeddingGenerator,
               });
            _collection = _vectorStore.GetCollection<ulong, T>(collectionname);

            _collection.EnsureCollectionExistsAsync().GetAwaiter().GetResult();
        }

        public async Task UpSert(string[] budgetInfo)
        {
            var records = budgetInfo.Select((input, index) => new T { Key = (ulong)index, Text = input });
            await _collection.UpsertAsync(records);
        }

        public async Task<List<VectorSearchResult<T>>> SearchAsync(string query)
        {
            VectorSearchOptions<T> options = new VectorSearchOptions<T>
            {
                Filter = null,
            };

            var results = new List<VectorSearchResult<T>>();
            await foreach (var result in _collection.SearchAsync(query, top: 1, options))
            {
                results.Add(result);
            }
            return results;
        }

        public QdrantCollection<ulong, T>? Collection => _collection;
    }
}
