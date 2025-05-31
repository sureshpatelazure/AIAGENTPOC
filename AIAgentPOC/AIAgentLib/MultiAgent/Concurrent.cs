using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Orchestration;
using Microsoft.SemanticKernel.Agents.Orchestration.Concurrent;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;


namespace AIAgentLib.MultiAgent
{
    public class Concurrent : IAgentOrchestration
    {   
        // To resolve SKEXP0110, you can suppress the diagnostic warning by adding the following pragma directive before and after the usage:

        public async Task<string[]> Run(List<ChatCompletionAgent> agents, string userInput)
        {
            #pragma warning disable SKEXP0110
            ConcurrentOrchestration concurrentOrchestration = new ConcurrentOrchestration(agents.ToArray());
           #pragma warning restore SKEXP0110

            // Run the orchestration
            InProcessRuntime runtime = new InProcessRuntime();
            await runtime.StartAsync();

            #pragma warning disable SKEXP0110
            var results = await concurrentOrchestration.InvokeAsync(userInput, runtime);
            #pragma warning restore SKEXP0110

            #pragma warning disable SKEXP0110
            string[] output = await results.GetValueAsync();
            #pragma warning restore SKEXP0110

            await runtime.RunUntilIdleAsync();

            return output;
        }
    }
}
