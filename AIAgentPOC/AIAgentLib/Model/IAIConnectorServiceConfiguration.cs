using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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