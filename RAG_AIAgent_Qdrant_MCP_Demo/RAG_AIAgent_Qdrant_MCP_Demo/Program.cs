using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using RAG_AIAgent_Qdrant_MCP_Demo;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration configuration = builder.Build();

Kernel kernel = ApplicationDemo.CreateSemanticKernel(configuration);

ApplicationDemo.CreateAndStoreEmbedding(kernel, configuration); 

//////
///

//IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator = kernel.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>();
//var qdranturi = configuration.GetSection("vectorstore:qdrant:uri").Get<string>();
//var qdrantapikey = configuration.GetSection("vectorstore:qdrant:apikey").Get<string>();
//var collectionname = configuration.GetSection("files:pdffiles:collectionname").Get<string>();

//QdrantVectorStoreService qdrantVectorStoreService = new  QdrantVectorStoreService(embeddingGenerator, qdranturi, qdrantapikey, collectionname);

//#pragma warning disable SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
//var textSearch = new VectorStoreTextSearch<RAG_AIAgent_Qdrant_MCP_Demo.DataLoader.DataContent>(qdrantVectorStoreService.Collection, embeddingGenerator);
//#pragma warning restore SKEXP0001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

//var searchPlugin = textSearch.CreateWithGetTextSearchResults("SearchPlugin");

//kernel.Plugins.Add(searchPlugin);

//PromptExecutionSettings promptExecutionSettings = new PromptExecutionSettings
//{
//    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
//};

//KernelArguments arguments = new(promptExecutionSettings);


//string query = string.Empty;

//while (query.ToLower() != "exit")
//{
//    Console.WriteLine("Enter your query (or type 'exit' to quit): ");
//    Console.WriteLine();
//    query = Console.ReadLine();
//    if (query == "exit")
//    {
//        break;
//    }

//    var result = await kernel.InvokePromptAsync(query, arguments);
//    Console.WriteLine($"AI Response: {result}");
//    Console.WriteLine();
//}

