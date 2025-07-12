using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using RAG_AIAgent_Qdrant_MCP_Demo;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

IConfiguration configuration = builder.Build();

Kernel kernel = ApplicationDemo.CreateSemanticKernel(configuration);

/// Create Embedding and Store in VectorStore   
/// //ApplicationDemo.CreateAndStoreEmbedding(kernel, configuration); 

//Chat with AI Agent using YAML configuration   
ApplicationDemo.ChatWithAIAgent(kernel, configuration);



