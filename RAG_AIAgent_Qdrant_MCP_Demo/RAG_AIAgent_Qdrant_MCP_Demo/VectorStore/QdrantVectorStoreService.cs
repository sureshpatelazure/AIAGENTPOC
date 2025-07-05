using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Qdrant.Client;
using RAG_AIAgent_Qdrant_MCP_Demo.DataLoader;

namespace RAG_AIAgent_Qdrant_MCP_Demo.VectorStore
{
    public class QdrantVectorStoreService
    {
        private QdrantVectorStore? _vectorStore;
        private QdrantCollection<ulong, TextSnippet>? _collection;

        public QdrantVectorStoreService(IEmbeddingGenerator embeddingGenerator, string uri, string apikey, string collectionname)
        {
            QdrantClient qdrantClient = new QdrantClient(new Uri(uri), apikey);

            _vectorStore = new QdrantVectorStore(qdrantClient, true,
               new QdrantVectorStoreOptions
               {
                   EmbeddingGenerator = embeddingGenerator,
               });
            _collection = _vectorStore.GetCollection<ulong, TextSnippet>(collectionname);

            _collection.EnsureCollectionExistsAsync().GetAwaiter().GetResult();
        }

        public async Task UpSert(IEnumerable<TextSnippet> textSnippets)
        {
            var data = textSnippets.Select(snippet => new TextSnippet
            {
                Key = snippet.Key,
                Text = snippet.Text, // You can modify the text as needed
            });

            
            await _collection.UpsertAsync(data);
        }

        //public async Task Search(string query)
        //{
        //    VectorSearchOptions<FinanceInfo> options = new VectorSearchOptions<FinanceInfo>
        //    {
        //        Filter = null, // No filter applie
        //    };

        //    var searchResult = _collection.SearchAsync(query, top: 1, options);
        //    var scoreThreshold = 0.9;
        //    await foreach (var result in searchResult)
        //    {
        //        if (result.Score >= scoreThreshold)
        //        {
        //            Console.WriteLine($"Key: {result.Record.Key}, Text: {result.Record.Text}, Score: {result.Score}");
        //        }
        //        else
        //        {
        //            Console.WriteLine($"No results found with score above {scoreThreshold}");
        //        }
        //    }

        //}

        public QdrantCollection<ulong, TextSnippet>? Collection => _collection;
    }
}
