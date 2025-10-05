using System.Collections.Generic;

namespace TokenFlow.AI.Chunking
{
    /// <summary>
    /// Defines a contract for splitting text into token-based chunks.
    /// </summary>
    public interface ITextChunker
    {
        /// <summary>
        /// Splits the given text into chunks, each containing at most <paramref name="maxTokens"/> tokens.
        /// Optionally, an overlap (in tokens) can be provided to retain context between chunks.
        /// </summary>
        /// <param name="text">Input text to split.</param>
        /// <param name="maxTokens">Maximum tokens per chunk.</param>
        /// <param name="overlapTokens">Optional number of overlapping tokens between chunks.</param>
        IReadOnlyList<Chunk> ChunkByTokens(string text, int maxTokens, int overlapTokens = 0);
    }
}

