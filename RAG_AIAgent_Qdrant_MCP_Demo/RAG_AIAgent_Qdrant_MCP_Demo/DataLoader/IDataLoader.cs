namespace RAG_AIAgent_Qdrant_MCP_Demo.DataLoader
{
     public interface IDataLoader
    {
        public Task<List<DataContent>> LoadData(string filePath, int blockDivision, int batchSize);
    }
}