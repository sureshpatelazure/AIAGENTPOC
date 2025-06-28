using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;

namespace MemoryVectorStorePOC.Embedd
{
    public static class EmbeddGenerator
    {
        public static IEmbeddingGenerator IEmbeddingGenerato(Kernel kernel)
        {
           return kernel.GetRequiredService<IEmbeddingGenerator<string,Embedding<float>>>();
        }
    }
}
