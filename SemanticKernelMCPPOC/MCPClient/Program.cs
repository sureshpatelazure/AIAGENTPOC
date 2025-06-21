

using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using ModelContextProtocol.Client;

await using IMcpClient mcpClient = await McpClientFactory.CreateAsync(new StdioClientTransport(new()
{
    Name = "MCPServer",
    // Point the client to the MCPServer server executable
    Command = Path.Combine("C:\\GenAI\\GitHub Project\\SemanticKernelMCPPOC\\MCPServer\\bin\\Debug\\net8.0\\MCPServer.exe")
}));

IList<McpClientTool> tools = await mcpClient.ListToolsAsync();

Console.WriteLine("Available MCP tools:");
foreach (var tool in tools)
{
    Console.WriteLine($"- Name: {tool.Name}, Description: {tool.Description}");
}
Console.WriteLine();

IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
kernelBuilder.Plugins.AddFromFunctions("Tools",tools.Select(aifunction=> aifunction.AsKernelFunction()));
#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
#pragma warning disable SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
kernelBuilder.Services.AddOllamaChatCompletion("llama3.2:latest", new Uri( "http://localhost:11434"));
#pragma warning restore SKEXP0070 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

Kernel kernel = kernelBuilder.Build();


// Enable automatic function calling
PromptExecutionSettings executionSettings = new()
{
    
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
};

Console.WriteLine("Sending Prompt");
// Execute a prompt using the MCP tools. The AI model will automatically call the appropriate MCP tools to answer the prompt.
var prompt = "What is the likely weather of North Eastham city today?";
var result = await kernel.InvokePromptAsync(prompt, new(executionSettings));
Console.WriteLine(result);


