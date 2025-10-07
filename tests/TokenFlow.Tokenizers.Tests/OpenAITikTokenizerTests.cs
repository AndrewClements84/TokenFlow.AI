using TokenFlow.Tokenizers.OpenAI;

namespace TokenFlow.Tokenizers.Tests
{
    public class OpenAITikTokenizerTests
    {
        [Fact]
        public void CountTokens_ShouldReturn_Positive_ForSimpleText()
        {
            var tokenizer = new OpenAITikTokenizer("gpt-4o-mini");
            int count = tokenizer.CountTokens("Hello world!");
            Assert.True(count > 0);
        }

        [Fact]
        public void EncodeDecode_ShouldRoundtrip_Text()
        {
            var tokenizer = new OpenAITikTokenizer("gpt-4o-mini");
            var tokens = tokenizer.Encode("Hello TokenFlow!");
            Assert.NotEmpty(tokens);

            var decoded = tokenizer.Decode(tokens);
            Assert.Contains("TokenFlow", decoded);
        }

        [Fact]
        public void CountTokens_ShouldHandle_ChatMessages()
        {
            var tokenizer = new OpenAITikTokenizer();
            var messages = new[] { ("user", "Hi there"), ("assistant", "Hello!") };
            int count = tokenizer.CountTokens(messages);
            Assert.True(count > 0);
        }
    }
}
