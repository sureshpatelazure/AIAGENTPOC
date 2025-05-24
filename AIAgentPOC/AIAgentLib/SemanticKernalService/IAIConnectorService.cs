using AIAgentLib.Model;
using Microsoft.SemanticKernel;

namespace AIAgentLib.SemanticKernalService
{
    public interface IAIServiceConnector
    {
        public Kernel BuildChatCompletion<T>(T connectorConfiguration) where T : AIConnectorServiceConfiguration;

    }
}