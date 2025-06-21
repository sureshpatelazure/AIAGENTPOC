using MCPClient;
using Microsoft.SemanticKernel;
using ModelContextProtocol.Client;

await using IMcpClient mcpClient = await McpClientFactory.CreateAsync(new StdioClientTransport(new()
{
    Name = "MCPServer",
    // Point the client to the MCPServer server executable
    Command = Path.Combine("C:\\GenAI\\GitHub Project\\SemanticKernelMCPPOC\\MCPServer\\bin\\Debug\\net8.0\\MCPServer.exe")
}));

IList<McpClientTool> tools = await mcpClient.ListToolsAsync();

//IMcpClient mcpClient = await MCPClient.MCPClient.CreateMCPClient();
//IList<McpClientTool> tools = await mcpClient.ListToolsAsync();

Kernel kernel= SemanticKernelService.CreateKernel(tools, "Ollama");

await ChatCompletion.StartChatCompletion(kernel); 