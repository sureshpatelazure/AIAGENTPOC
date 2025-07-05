using RAG_AIAgent_Qdrant_MCP_Demo.DataLoader;
using Microsoft.Extensions.Configuration;


var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


IConfiguration configuration = builder.Build();

// Read the values
var filePaths = configuration.GetSection("fileconfiguration:pdffileconfiguration:filepath").Get<string[]>();
var batchSize = configuration.GetValue<int>("fileconfiguration:pdffileconfiguration:batchSize");

PDFLoader pDFLoader = new PDFLoader();

foreach (var path in filePaths)
{
    var pdfdata = pDFLoader.LoadPDF(path, batchSize).GetAwaiter().GetResult();
}




