using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Orchestration;
using Microsoft.SemanticKernel.Agents.Orchestration.Concurrent;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;


namespace AIAgentLib.MultiAgent
{
    public class Concurrent
    {   
        // To resolve SKEXP0110, you can suppress the diagnostic warning by adding the following pragma directive before and after the usage:

        public async Task Run(List<ChatCompletionAgent> agents)
        {
            #pragma warning disable SKEXP0110
            ConcurrentOrchestration concurrentOrchestration = new ConcurrentOrchestration(agents.ToArray());
           #pragma warning restore SKEXP0110

            // Run the orchestration
            InProcessRuntime runtime = new InProcessRuntime();
            await runtime.StartAsync();

            #pragma warning disable SKEXP0110
            var results = await concurrentOrchestration.InvokeAsync("What is temperature?", runtime);
            #pragma warning restore SKEXP0110

            #pragma warning disable SKEXP0110
            string[] output = await results.GetValueAsync();
            #pragma warning restore SKEXP0110

            Console.WriteLine($"# RESULT:\n{string.Join("\n\n", output.Select(text => $"{text}"))}");

            await runtime.RunUntilIdleAsync();
        }
    }
}
