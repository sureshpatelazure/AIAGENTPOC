using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;

namespace AIAgentLib.AIAgent
{
    public class AIAgent : IAIAgent
    {
        public ChatCompletionAgent CreateAIAgent(Kernel kernel, KernelArguments kernelArgument, string yamlContent)
        {
            PromptTemplateConfig templateConfig = new PromptTemplateConfig(yamlContent);
            KernelPromptTemplateFactory templateFactory = new KernelPromptTemplateFactory();

            ChatCompletionAgent agent = new(templateConfig, templateFactory)
            {
                Kernel = kernel,
                Arguments = new KernelArguments(
                     new PromptExecutionSettings
                     {
                         FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                     }
                )
            };

            return agent;
        }
    }
}