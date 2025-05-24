using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;

namespace AIAgentLib.SemanticKernalService
{
    public interface IAIConnectorService
    {
        public Kernel BuildChatCompletion(IConfiguration configuration);
        public Kernel BuildChatCompletion(IConfiguration configuration, List<Object> Plugins);

    }
}
