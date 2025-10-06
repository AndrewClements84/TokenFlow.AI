using System;
using TokenFlow.AI.Chunking;
using TokenFlow.AI.Tokenizer;

namespace TokenFlow.Tools.Commands
{
    public static class ChunkCommand
    {
        public static int Run(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Please provide text to chunk.");
                return 1;
            }

            var tokenizer = new ApproxTokenizer();
            var chunker = new TokenChunker(tokenizer);

            // TODO: wire real options later; these are sensible defaults for now
            var chunks = chunker.ChunkByTokens(text, 100, 10);
            int index = 1;

            foreach (var chunk in chunks)
            {
                var preview = chunk.Text.Substring(0, Math.Min(50, chunk.Text.Length));
                Console.WriteLine($"Chunk {index++}: {preview}...");
            }

            Console.WriteLine("Total chunks: " + chunks.Count);
            return 0;
        }
    }
}

