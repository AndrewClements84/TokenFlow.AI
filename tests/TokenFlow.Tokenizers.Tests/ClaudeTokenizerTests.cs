using TokenFlow.Tokenizers.Claude;

namespace TokenFlow.Tokenizers.Tests
{
    public class ClaudeTokenizerTests
    {
        [Fact]
        public void CountTokens_ShouldReturn_Positive_ForSimpleText()
        {
            var tokenizer = new ClaudeTokenizer("claude-3-opus");
            int count = tokenizer.CountTokens("Test prompt for Claude models.");
            Assert.True(count > 0);
        }

        [Fact]
        public void EncodeDecode_ShouldRoundtrip_Text()
        {
            var tokenizer = new ClaudeTokenizer("claude-3-opus");
            var tokens = tokenizer.Encode("Hello world from Claude!");
            Assert.NotEmpty(tokens);

            var decoded = tokenizer.Decode(tokens);
            Assert.Contains("Hello", decoded);
        }

        [Fact]
        public void CountTokens_ShouldHandle_ChatMessages()
        {
            var tokenizer = new ClaudeTokenizer();
            var messages = new[] { ("user", "Hello"), ("assistant", "Hi there!") };
            int count = tokenizer.CountTokens(messages);
            Assert.True(count > 0);
        }

        [Fact]
        public void Constructor_ShouldDefaultName_WhenModelIdIsNull()
        {
            var tokenizer = new ClaudeTokenizer(null);
            Assert.Equal("claude-3-opus", tokenizer.Name);
        }

        [Fact]
        public void CountTokens_ShouldReturnZero_WhenTextIsNullOrEmpty()
        {
            var tokenizer = new ClaudeTokenizer();
            Assert.Equal(0, tokenizer.CountTokens((string)null));
            Assert.Equal(0, tokenizer.CountTokens(string.Empty));
        }

        [Fact]
        public void CountTokens_ShouldReturnZero_WhenMessagesIsNull()
        {
            var tokenizer = new ClaudeTokenizer();
            Assert.Equal(0, tokenizer.CountTokens((IEnumerable<(string role, string content)>)null));
        }

        [Fact]
        public void Encode_ShouldReturnEmptyArray_WhenTextIsNullOrEmpty()
        {
            var tokenizer = new ClaudeTokenizer();
            var result1 = tokenizer.Encode(null);
            var result2 = tokenizer.Encode(string.Empty);

            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.Empty(result1);
            Assert.Empty(result2);
        }

        [Fact]
        public void Decode_ShouldReturnEmptyString_WhenTokensIsNull()
        {
            var tokenizer = new ClaudeTokenizer();
            var result = tokenizer.Decode(null);
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Decode_ShouldHandle_InvalidIntegerTokens()
        {
            var tokenizer = new ClaudeTokenizer();
            var tokens = new List<string> { "abc", "123", "xyz" };

            var decoded = tokenizer.Decode(tokens);

            Assert.NotNull(decoded);
            Assert.True(decoded.Length > 0);
        }

    }
}

