using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using RAG_AIAgent_Qdrant_MCP_Demo.DataLoader;
using RAG_AIAgent_Qdrant_MCP_Demo.SemanticKernelCore;


var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


IConfiguration configuration = builder.Build();

// Read the values
var filePaths = configuration.GetSection("files:pdffiles:filepath").Get<string[]>();
var batchSize = configuration.GetValue<int>("files:pdffiles:batchSize");

PDFLoader pDFLoader = new PDFLoader();

//foreach (var path in filePaths)
//{
//    var pdfdata = pDFLoader.LoadPDF(path, batchSize).GetAwaiter().GetResult();
//}

var qdranturi = configuration.GetSection("vectorstore:qdrant:uri").Get<string>();
var apikey = configuration.GetSection("vectorstore:qdrant:apikey").Get<string>();

var embeddingModel = configuration.GetSection("aiservice:embedding:ollama:model").Get<string>();
var embeddingApiUrl = configuration.GetSection("aiservice:embedding:ollama:uri").Get<string>();

var chatModel = configuration.GetSection("aiservice:chatcompletion:ollama:model").Get<string>();
var chatApiUrl = configuration.GetSection("aiservice:chatcompletion:ollama:uri").Get<string>();

SemanticKernelService semanticKernelService = new SemanticKernelService();
Kernel kernel =  semanticKernelService.CreateKernelWithAIService(embeddingModel, embeddingApiUrl, chatModel, chatApiUrl);

//////
///

string query = string.Empty;

while (query.ToLower() != "exit")
{
    Console.WriteLine("Enter your query (or type 'exit' to quit): ");
    Console.WriteLine();
    query = Console.ReadLine();
    if (query == "exit")
    {
        break;
    }

    var result = await kernel.InvokePromptAsync(query);
    Console.WriteLine($"AI Response: {result}");
    Console.WriteLine();
}

