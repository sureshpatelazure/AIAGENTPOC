using MemoryVectorStorePOC.DataModel;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Connectors.Qdrant;
using Microsoft.SemanticKernel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryVectorStorePOC.TextSearch
{
    public static class TextSearchService
    {
        public static async Task Search(string query, QdrantCollection<ulong, FinanceInfo> collection , IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator)
        {
            #pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            var textSearch = new VectorStoreTextSearch<FinanceInfo>(collection, embeddingGenerator);
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            KernelSearchResults<TextSearchResult> textResults = await textSearch.GetTextSearchResultsAsync(query, new() { Top = 2, Skip = 0 });

            await foreach (TextSearchResult result in textResults.Results)
            {
                Console.WriteLine($"Key: {result.Name}, Text: {result.Value}");
            }   
        }
    }
}
