using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAG_AIAgent_Qdrant_MCP_Demo.DataLoader
{
    public sealed class TextSnippet
    {
        [VectorStoreKey]
        public required Guid Key { get; set; }

        [TextSearchResultValue]
        [VectorStoreData]
        public string? Text { get; set; }

        [TextSearchResultName]
        [VectorStoreData]
        public string? ReferenceDescription { get; set; }

        [TextSearchResultLink]
        [VectorStoreData]
        public string? ReferenceLink { get; set; }

        /// <summary>
        /// The text embedding for this snippet. This is used to search the vector store.
        /// While this is a string property it has the vector attribute, which means whatever
        /// text it contains will be converted to a vector and stored as a vector in the vector store.
        /// </summary>
        [VectorStoreVector(1536)]
        public string? TextEmbedding => this.Text;
    }
}
