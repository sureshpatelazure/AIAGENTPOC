using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace AIAgentPOC.SemanticKernal
{
    public interface IAIConnectorService
    {
        public Kernel BuildChatCompletionKernel(IConfiguration configuration);
        public Kernel BuildChatCompletionKernelWithPlugin(IConfiguration configuration, List<Object> Plugins);
    }
}
