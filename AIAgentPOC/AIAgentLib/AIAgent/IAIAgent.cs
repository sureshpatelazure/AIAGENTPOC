using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAgentLib.AIAgent
{
    public interface IAIAgent
    {
        public ChatCompletionAgent CreateAIAgent(Kernel kernel,KernelArguments kernelArgument, string yamlContent);
    }
}
