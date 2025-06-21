using MCPServer.Extension;
using MCPServer.Plugin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;

namespace MCPServer
{
    public static class SemanticKernelService
    {
        public static async Task CreateAndStartMCPServer() {
            
            IKernelBuilder kernelBuilder = Kernel.CreateBuilder();

            kernelBuilder.Plugins.AddFromType<DateTimeUtils>();
            kernelBuilder.Plugins.AddFromType<WeatherUtils>();

            Kernel kernel = kernelBuilder.Build();

            var builder = Host.CreateEmptyApplicationBuilder(settings: null);
            builder.Services
                .AddMcpServer()
                .WithStdioServerTransport()
                .WithTools(kernel.Plugins);

            Console.WriteLine("Starting MCP Server...");
            await builder.Build().RunAsync();
        }
    }
}