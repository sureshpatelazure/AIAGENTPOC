using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;
using MCPServer;

IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.Plugins.AddFromType<MCPClient.Plugin.DateTimeUtils>();
kernelBuilder.Plugins.AddFromType<MCPClient.Plugin.WeatherUtils>();

Kernel kernel = kernelBuilder.Build();

Console.WriteLine("Kernel initialized with plugins.");

var builder = Host.CreateEmptyApplicationBuilder(settings: null);
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools(kernel.Plugins);

Console.WriteLine("MCP Server Started");
await builder.Build().RunAsync();






