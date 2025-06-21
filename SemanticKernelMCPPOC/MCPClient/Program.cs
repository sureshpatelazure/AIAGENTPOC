using MCPClient;
using Microsoft.SemanticKernel;
using ModelContextProtocol.Client;

Console.WriteLine("Starting MCP Client...");    

IList<McpClientTool> tools = await MCPClient.MCPClient.CreateMCPClient();

Kernel kernel= SemanticKernelService.CreateKernel(tools, "Ollama");

await ChatCompletion.StartChatCompletion(kernel); 