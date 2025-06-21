using ModelContextProtocol.Client;

namespace MCPClient
{
    public static class MCPClient
    {
        public static async Task<IList<McpClientTool>> CreateMCPClient()
        {
            IMcpClient mcpClient = await McpClientFactory.CreateAsync(new StdioClientTransport(new()
            {
                Name = Common.GetConfiguration("MCPServerName"),
                Command = Common.GetConfiguration("MCPServerPath")
            }));

            return  await mcpClient.ListToolsAsync(); ;
        }
    }
}