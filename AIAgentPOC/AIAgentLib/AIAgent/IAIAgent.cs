using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;

namespace AIAgentLib.AIAgent
{
    public interface IAIAgent
    {
        public ChatCompletionAgent CreateAIAgent(Kernel kernel,KernelArguments kernelArgument, string yamlContent);
    }
}
