namespace AIAgentLib.Model
{
    public abstract class AIConnectorServiceConfiguration
    {

    }

    public class OllamaConnectorServiceConfiguration : AIConnectorServiceConfiguration
    {
        public string ModelId { get; set; }
        public string Uri { get; set; }
        public  bool useEmbeddingModel { get; set; } = false;
        public string EmbeddingModelId { get; set; }
        public string EmbeddingUrl { get; set; }
    }

    public class HuggingFaceConnectorServiceConfiguration : AIConnectorServiceConfiguration
    {
        public string ModelId { get; set; }
        public string Uri { get; set; }
        public string ApiKey { get; set; }
    }

    public enum AIConnectorServiceType
    {
        Ollama,
        HuggingFace,
        AzureOpenAI,
        OpenAI
    }  
    public class EmbeddingConfiguration
    {
        public string CollectionName { get; set; }
        public string DocumentContent { get; set; }
    }   
}