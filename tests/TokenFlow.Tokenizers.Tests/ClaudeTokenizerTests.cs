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
    }
}

