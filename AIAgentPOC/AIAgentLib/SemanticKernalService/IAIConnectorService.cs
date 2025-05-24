using AIAgentLib.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;

namespace AIAgentLib.SemanticKernalService
{
    public interface IAIConnectorService
    {
        public Kernel BuildChatCompletion<T>(T connectorConfiguration) where T : AIConnectorServiceConfiguration;
        

    }
}
