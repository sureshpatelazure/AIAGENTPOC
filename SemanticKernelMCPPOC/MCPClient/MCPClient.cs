using ModelContextProtocol.Client;

namespace MCPClient
{
    public static class MCPClient
    {
        public static async Task<IMcpClient> CreateMCPClient()
        {
            //await using IMcpClient mcpClient = await McpClientFactory.CreateAsync(new StdioClientTransport(new()
            //{
            //    Name = Common.GetConfiguration("MCPServerName"),
            //    Command = Common.GetConfiguration("MCPServerPath")
            //}));

            await using IMcpClient mcpClient = await McpClientFactory.CreateAsync(new StdioClientTransport(new()
            {
                Name = "MCPServer",
                // Point the client to the MCPServer server executable
                Command = Path.Combine("C:\\GenAI\\GitHub Project\\SemanticKernelMCPPOC\\MCPServer\\bin\\Debug\\net8.0\\MCPServer.exe")
            }));

            return mcpClient;
            //return  await mcpClient.ListToolsAsync(); ;
        }
    }
}