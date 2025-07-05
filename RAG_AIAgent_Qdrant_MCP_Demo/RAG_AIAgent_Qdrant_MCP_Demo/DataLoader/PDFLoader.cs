using Microsoft.Extensions.VectorData;
using Microsoft.SemanticKernel.ChatCompletion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;

namespace RAG_AIAgent_Qdrant_MCP_Demo.DataLoader
{
    public class PDFLoader
    {
        public async Task <List<List<TextSnippet>>> LoadPDF(string filePath, int batchSize)
        {
            List<RawContent> rawContents = new List<RawContent>();      

            using (PdfDocument document = PdfDocument.Open(filePath))
            {
                foreach (Page page in document.GetPages()) { 

                    var blocks = DefaultPageSegmenter.Instance.GetBlocks(page.GetWords());
                    foreach (var block in blocks) {
                        rawContents.Add(new RawContent {
                            Text = block.Text,
                            PageNumber = page.Number
                        });
                    }
                }
            }

            var batches = rawContents.Chunk(batchSize);

            List<List<TextSnippet>> textSnippetList = new List<List<TextSnippet>>();

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
                ulong counter = 0;
                var records = textContent.Select(content => new TextSnippet()
                {
                    Key = ++counter,
                    Text = content.Text,
                });

                textSnippetList.Add(records.ToList());
            }

            return textSnippetList;
        }
    }

    public sealed class RawContent
    {
        public string? Text { get; init; }

        public ReadOnlyMemory<byte>? Image { get; init; }

        public int PageNumber { get; init; }
    }
}
