using TokenFlow.Tokenizers.Shared;

namespace TokenFlow.Tokenizers.Tests
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
        public void CountTokens_ShouldReturnZero_ForEmptyText()
        {
            // Arrange
            var tokenizer = new ApproxTokenizer();

            // Act
            int count = tokenizer.CountTokens(string.Empty);

            // Assert
            Assert.Equal(0, count);
        }

        [Fact]
        public void CountTokens_ShouldReturnExpectedCount_ForNormalSentence()
        {
            // Arrange
            var tokenizer = new ApproxTokenizer();
            string text = "Hello world! This is TokenFlow.AI.";

            // Act
            int count = tokenizer.CountTokens(text);

            // Assert
            Assert.True(count > 0);
            Assert.Equal(tokenizer.Encode(text).Count, count);
        }

        [Fact]
        public void CountTokens_Messages_ShouldReturnSumOfEachMessage()
        {
            // Arrange
            var tokenizer = new ApproxTokenizer();
            var messages = new[]
            {
                ("system", "You are a helpful assistant."),
                ("user", "What is the weather today?")
            };

            // Act
            int count = tokenizer.CountTokens(messages);

            // Assert
            Assert.True(count > 0);
        }

        [Fact]
        public void CountTokens_Messages_ShouldReturnZero_WhenMessagesIsNull()
        {
            // Arrange
            var tokenizer = new ApproxTokenizer();

            // Act
            int count = tokenizer.CountTokens((IEnumerable<(string role, string content)>)null);

            // Assert
            Assert.Equal(0, count);
        }

        [Fact]
        public void Encode_ShouldReturnTokens_WhenTextIsValid()
        {
            // Arrange
            var tokenizer = new ApproxTokenizer();
            string text = "TokenFlow.AI works well!";

            // Act
            var tokens = tokenizer.Encode(text);

            // Assert
            Assert.NotNull(tokens);
            Assert.NotEmpty(tokens);
            Assert.Contains("TokenFlow", string.Join("", tokens));
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
        public void Decode_ShouldRecombineTokens_IntoSentence()
        {
            // Arrange
            var tokenizer = new ApproxTokenizer();
            var tokens = new List<string> { "Hello", "world", "!" };

            // Act
            var text = tokenizer.Decode(tokens);

            // Assert
            Assert.Contains("Hello", text);
            Assert.Contains("world", text);
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
        public void Decode_ShouldHandleEmptyTokenList()
        {
            // Arrange
            var tokenizer = new ApproxTokenizer();
            var tokens = new List<string>();

            // Act
            var text = tokenizer.Decode(tokens);

            // Assert
            Assert.Equal(string.Empty, text.Trim());
        }

        [Fact]
        public void CountTokens_ShouldHandlePunctuationProperly()
        {
            // Arrange
            var tokenizer = new ApproxTokenizer();
            string text = "Hi! How's this? Good.";

            // Act
            int count = tokenizer.CountTokens(text);

            // Assert
            Assert.True(count > 3); // should split punctuation separately
        }

        [Fact]
        public void CountTokens_ShouldIgnore_ExtraWhitespace()
        {
            var tokenizer = new ApproxTokenizer();
            string text = "Hello     world   !";
            int count = tokenizer.CountTokens(text);
            Assert.Equal(tokenizer.Encode("Hello world !").Count, count);
        }
    }
}
