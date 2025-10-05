using Xunit;
using TokenFlow.AI.Chunking;

namespace TokenFlow.AI.Tests.Chunking
{
    public class ChunkTests
    {
        [Fact]
        public void Constructor_ShouldAssign_AllPropertiesCorrectly()
        {
            // Arrange
            string text = "Hello world";
            int tokenCount = 5;
            int startIndex = 10;
            int length = text.Length;

            // Act
            var chunk = new Chunk(text, tokenCount, startIndex, length);

            // Assert
            Assert.Equal(text, chunk.Text);
            Assert.Equal(tokenCount, chunk.TokenCount);
            Assert.Equal(startIndex, chunk.StartIndex);
            Assert.Equal(length, chunk.Length);
        }

        [Fact]
        public void Properties_ShouldBeImmutableAfterConstruction()
        {
            // Arrange
            var chunk = new Chunk("immutable", 3, 0, 9);

            // Act & Assert
            Assert.Equal("immutable", chunk.Text);
            Assert.Equal(3, chunk.TokenCount);
            Assert.Equal(0, chunk.StartIndex);
            Assert.Equal(9, chunk.Length);
        }
    }
}

