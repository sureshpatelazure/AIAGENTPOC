namespace RAG_AIAgent_Qdrant_MCP_Demo.VectorStore
{
    public interface IVectorStoreService 
    {
        public Task UpSert(IEnumerable<DataLoader.DataContent> dataContents);
    }
}
