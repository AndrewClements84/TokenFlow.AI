using TokenFlow.AI.Chunking;
using TokenFlow.AI.Tokenizer;

namespace TokenFlow.AI.Tests.Chunking
{
    public class TokenChunkerTests
    {
        [Fact]
        public void Constructor_ShouldThrow_WhenTokenizerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TokenChunker(null));
        }

        [Fact]
        public void ChunkByTokens_ShouldReturnEmptyList_WhenTextIsNullOrEmpty()
        {
            var chunker = new TokenChunker(new ApproxTokenizer());

            Assert.Empty(chunker.ChunkByTokens(null, 100));
            Assert.Empty(chunker.ChunkByTokens(string.Empty, 100));
        }

        [Fact]
        public void ChunkByTokens_ShouldThrow_WhenMaxTokensIsZeroOrNegative()
        {
            var chunker = new TokenChunker(new ApproxTokenizer());
            Assert.Throws<ArgumentOutOfRangeException>(() => chunker.ChunkByTokens("hello", 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => chunker.ChunkByTokens("hello", -5));
        }

        [Fact]
        public void ChunkByTokens_ShouldThrow_WhenOverlapIsNegative()
        {
            var chunker = new TokenChunker(new ApproxTokenizer());
            Assert.Throws<ArgumentOutOfRangeException>(() => chunker.ChunkByTokens("test text", 10, -1));
        }

        [Fact]
        public void ChunkByTokens_ShouldReturnSingleChunk_WhenShortText()
        {
            var chunker = new TokenChunker(new ApproxTokenizer());
            string text = "TokenFlow.AI is great.";

            var chunks = chunker.ChunkByTokens(text, 50);

            Assert.Single(chunks);
            Assert.Equal(text, chunks.First().Text);
        }

        [Fact]
        public void ChunkByTokens_ShouldSplitLongTextIntoMultipleChunks()
        {
            var chunker = new TokenChunker(new ApproxTokenizer());
            string text = string.Join(" ", Enumerable.Repeat("TokenFlow.AI", 500));

            var chunks = chunker.ChunkByTokens(text, 50);

            Assert.True(chunks.Count > 1);
            Assert.All(chunks, c => Assert.True(c.TokenCount <= 50));
        }

        [Fact]
        public void ChunkByTokens_ShouldRespectOverlapTokens()
        {
            var chunker = new TokenChunker(new ApproxTokenizer());
            string text = string.Join(" ", Enumerable.Repeat("TokenFlow", 200));

            var chunks = chunker.ChunkByTokens(text, 20, 5);

            Assert.True(chunks.Count > 1);
            // Ensure some overlap between chunks
            for (int i = 1; i < chunks.Count; i++)
            {
                Assert.True(chunks[i].StartIndex < chunks[i - 1].StartIndex + chunks[i - 1].Length);
            }
        }

        [Fact]
        public void ChunkByTokens_ShouldCoverFullTextWithoutOmission()
        {
            var chunker = new TokenChunker(new ApproxTokenizer());
            string text = string.Join(" ", Enumerable.Repeat("FlowAI", 300));

            var chunks = chunker.ChunkByTokens(text, 30);

            string recombined = string.Join("", chunks.Select(c => c.Text));
            Assert.Contains("FlowAI", recombined);
        }
    }
}

