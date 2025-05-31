using Microsoft.SemanticKernel.Agents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIAgentLib.MultiAgent
{
    public interface IAgentOrchestration
    {
        public Task<string[]> Run(List<ChatCompletionAgent> agents, string userInput);
    }
}
