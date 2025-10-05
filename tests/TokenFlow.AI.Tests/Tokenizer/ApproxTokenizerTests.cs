using TokenFlow.AI.Tokenizer;

namespace TokenFlow.AI.Tests.Tokenizer
{
    public class ApproxTokenizerTests
    {
        [Fact]
        public void Name_ShouldReturn_Approx()
        {
            // Arrange
            var tokenizer = new ApproxTokenizer();

            // Act
            var name = tokenizer.Name;

            // Assert
            Assert.Equal("approx", name);
        }

        [Fact]
        public void CountTokens_ShouldReturnZero_WhenMessagesIsNull()
        {
            // Arrange
            var tokenizer = new ApproxTokenizer();

            // Act
            int count = tokenizer.CountTokens((IEnumerable<(string role, string content)>)null);

            // Assert
            Assert.Equal(0, count);
        }

        [Fact]
        public void Encode_ShouldReturnEmptyList_WhenTextIsNullOrEmpty()
        {
            // Arrange
            var tokenizer = new ApproxTokenizer();

            // Act
            var result1 = tokenizer.Encode(null);
            var result2 = tokenizer.Encode(string.Empty);

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Empty(result1);
            Assert.Empty(result2);
        }

        [Fact]
        public void Decode_ShouldReturnEmptyString_WhenTokensIsNull()
        {
            // Arrange
            var tokenizer = new ApproxTokenizer();

            // Act
            var result = tokenizer.Decode(null);

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void CountTokens_ShouldHandleRegularInput()
        {
            // Arrange
            var tokenizer = new ApproxTokenizer();
            string text = "This is TokenFlow.AI!";

            // Act
            int tokens = tokenizer.CountTokens(text);

            // Assert
            Assert.True(tokens > 0);
        }
    }
}
