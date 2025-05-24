using AIAgentLib.Model;
using AIAgentLib.SemanticKernalService;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAgentLib
{
    public class ChatCompletionStartup
    {
        public ChatCompletionStartup(AIConnectorServiceType aIConnectorServiceType, AIConnectorServiceConfiguration aIConnectorServiceConfiguration)
        {

            CreateKernel(aIConnectorServiceType, aIConnectorServiceConfiguration);

        }
        private Kernel CreateKernel(AIConnectorServiceType aIConnectorServiceType, AIConnectorServiceConfiguration aIConnectorServiceConfiguration )
        {
            if (aIConnectorServiceType == AIConnectorServiceType.Ollama)
            {
                IAIConnectorService aIConnectorService = new OllamaKernelService();
                return aIConnectorService.BuildChatCompletion(aIConnectorServiceConfiguration);

            }
            else
            {
                throw new ArgumentException($"Unsupported AI connector service type: {aIConnectorServiceType}");
            }
           
        }

    }
}

 
