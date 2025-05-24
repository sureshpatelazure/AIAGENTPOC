namespace AIAgentLib.Model
{
    public abstract class AIConnectorServiceConfiguration
    {

    }

    public class OllamaConnectorServiceConfiguration : AIConnectorServiceConfiguration
    {
        public string ModelId { get; set; }
        public string Uri { get; set; }
    }

    public enum AIConnectorServiceType
    {
        Ollama,
        AzureOpenAI,
        OpenAI
    }   
}