using RAG_AIAgent_Qdrant_MCP_Demo.DataLoader;

PDFLoader pDFLoader = new PDFLoader();
var pdfdata = pDFLoader.LoadPDF("C:\\GenAI\\semantic-kernel.pdf", 10).GetAwaiter().GetResult();
var test = 0;