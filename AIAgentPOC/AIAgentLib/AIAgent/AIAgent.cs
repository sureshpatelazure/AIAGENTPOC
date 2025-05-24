using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Arguments = kernelArgument
            };

            return agent;
        }
    }
}