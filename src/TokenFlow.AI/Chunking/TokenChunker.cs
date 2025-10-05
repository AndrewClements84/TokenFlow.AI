using System;
using System.Collections.Generic;
using TokenFlow.Core.Interfaces;

namespace TokenFlow.AI.Chunking
{
    /// <summary>
    /// Default implementation of a text chunker that splits text into segments
    /// based on token counts using a provided ITokenizer.
    /// </summary>
    public class TokenChunker : ITextChunker
    {
        private readonly ITokenizer _tokenizer;

        public TokenChunker(ITokenizer tokenizer)
        {
            if (tokenizer == null)
                throw new ArgumentNullException(nameof(tokenizer));

            _tokenizer = tokenizer;
        }

        public IReadOnlyList<Chunk> ChunkByTokens(string text, int maxTokens, int overlapTokens = 0)
        {
            var chunks = new List<Chunk>();

            if (string.IsNullOrEmpty(text))
                return chunks;

            if (maxTokens <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxTokens), "maxTokens must be positive.");

            if (overlapTokens < 0)
                throw new ArgumentOutOfRangeException(nameof(overlapTokens), "overlapTokens cannot be negative.");

            int position = 0;
            int estimatedCharsPerToken = Math.Max(4, text.Length / Math.Max(1, _tokenizer.CountTokens(text)));

            while (position < text.Length)
            {
                // Take a slice
                int sliceLength = Math.Min(text.Length - position, estimatedCharsPerToken * maxTokens);
                string slice = text.Substring(position, sliceLength);

                // Shrink until it fits
                int tokenCount = _tokenizer.CountTokens(slice);
                while (tokenCount > maxTokens && sliceLength > 1)
                {
                    sliceLength -= estimatedCharsPerToken;
                    if (sliceLength <= 0) break;
                    slice = text.Substring(position, Math.Min(sliceLength, text.Length - position));
                    tokenCount = _tokenizer.CountTokens(slice);
                }

                var chunk = new Chunk(slice, tokenCount, position, sliceLength);
                chunks.Add(chunk);

                if (position + sliceLength >= text.Length)
                    break;

                int overlapCharCount = Math.Min(overlapTokens * estimatedCharsPerToken, sliceLength);
                position += sliceLength - overlapCharCount;
            }

            return chunks;
        }
    }
}

