using AIAgentLib.Model;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.InMemory;
using Microsoft.SemanticKernel.Data;

namespace AIAgentLib.RAGAIService
{
    public class RAGService
    {
        public RAGService()
        {
            // Initialize any necessary components or services here
        }

        #pragma warning disable SKEXP0130 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        public async Task<TextSearchProvider> AddDocumentAsync(Kernel kernel, EmbeddingConfiguration embeddingConfiguration)
        #pragma warning restore SKEXP0130 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        {
            // 1. Get the embedding generator from the kernel's service provider (use the new interface)
            var embeddingGenerator = kernel.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>();

            // 2. Create a vector store to store documents (replace with MemoryVectorStore)
            var vectorStore = new InMemoryVectorStore(new() { EmbeddingGenerator = embeddingGenerator });

            // 3. Create a TextSearchStore for storing and searching text documents
                #pragma warning disable SKEXP0130 // Suppress evaluation warning for TextSearchStore
                    using var textSearchStore = new TextSearchStore<string>(vectorStore, collectionName: embeddingConfiguration.CollectionName, vectorDimensions: 1536);
                #pragma warning restore SKEXP0130

            // 4. Upsert documents into the store
            await textSearchStore.UpsertTextAsync(new[]
            {
                    embeddingConfiguration.DocumentContent
            });

            #pragma warning disable SKEXP0130 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
                var textSearchProvider = new TextSearchProvider(textSearchStore);
            #pragma warning restore SKEXP0130 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

            return textSearchProvider;  
        }
    }
}
