using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;

namespace AIAgentLib.SemanticKernalService
{
    public class OllamaKernelService : IAIConnectorService
    {
        public Kernel BuildChatCompletion(IConfiguration configuration)
        {
            var builder = Kernel.CreateBuilder();
            return builder.Build();
        }

        public Kernel BuildChatCompletion(IConfiguration configuration, List<object> Plugins)
        {
            var builder = Kernel.CreateBuilder();
            return builder.Build();
        }
    }
}
