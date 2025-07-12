using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;

namespace RAG_AIAgent_Qdrant_MCP_Demo.DataLoader
{
    public class PDFLoader : IDataLoader
    {
        public async Task<List<DataContent>> LoadData(string filePath, int blockDivision, int batchSize)
        {
            List<RawContent> rawContents = new();

            Int16 pageCounter = 1;

            using (PdfDocument document = PdfDocument.Open(filePath))
            {
                foreach (Page page in document.GetPages())
                {

                    var blocks = DefaultPageSegmenter.Instance.GetBlocks(page.GetWords());

                    foreach (var block in blocks)
                    {
                        if (!string.IsNullOrEmpty(block.Text))
                        {
                            int textLength = block.Text.Length;
                            int partSize = textLength / blockDivision;
                            int remainder = textLength % blockDivision;
                            int start = 0;

                            for (int i = 0; i < blockDivision; i++)
                            {
                                // Distribute the remainder among the first 'remainder' parts
                                int currentPartSize = partSize + (i < remainder ? 1 : 0);

                                string part = block.Text.Substring(start, currentPartSize);

                                rawContents.Add(new RawContent
                                {
                                    Text = part,
                                    PageNumber = pageCounter++
                                });

                                start += currentPartSize;
                            }
                        }
                    }
                }
            }

            var batches = rawContents.Chunk(batchSize);

            List<DataContent> dataContents = new();

            ulong counter = 0;
            foreach (var batch in batches)
            {
                var textContentTasks = batch.Select(async content =>
                {
                    if (content.Text != null)
                    {
                        return content;
                    }
                    else
                    {
                        return new RawContent
                        {
                            Text = "No content found",
                            PageNumber = content.PageNumber
                        };
                    }
                });

                var textContent = await Task.WhenAll(textContentTasks).ConfigureAwait(false);

                var records = textContent.Select(content => new DataContent()
                {
                    Key = ++counter,
                    Text = content.Text,
                });

                // Add each record individually
                foreach (var record in records)
                {
                    dataContents.Add(record);
                }
            }

            return dataContents;
        }
    }
}
