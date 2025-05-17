using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAgentPOC.SemanticKernal
{
    public class HuggingFace : IAIConnectorService
    {
        public Kernel BuildChatCompletionKernel(IConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public Kernel BuildChatCompletionKernelWithPlugin(IConfiguration configuration, List<object> Plugins)
        {
            throw new NotImplementedException();
        }
    }
}
