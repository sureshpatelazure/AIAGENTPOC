namespace RAG_AIAgent_Qdrant_MCP_Demo.VectorStore
{
    public interface IVectorRecord
    {
        ulong Key { get; set; }
        string Text { get; set; }
    }
}