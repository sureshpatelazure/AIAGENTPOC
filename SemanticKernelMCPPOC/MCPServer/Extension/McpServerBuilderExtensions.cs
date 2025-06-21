using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using ModelContextProtocol.Server;

namespace MCPServer.Extension
{
    public static class McpServerBuilderExtensions
    {
        public static IMcpServerBuilder WithTools(this IMcpServerBuilder builder, KernelPluginCollection plugins)
        {
            foreach (var plugin in plugins)
            {
                foreach (var function in plugin)
                {
                    builder.Services.AddSingleton(services => McpServerTool.Create(function));
                }
            }
            return builder;
        }
    }
}