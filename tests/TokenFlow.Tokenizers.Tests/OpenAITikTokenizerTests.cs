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

        [Fact]
        public void Constructor_ShouldDefaultName_WhenModelIdIsNull()
        {
            var tokenizer = new OpenAITikTokenizer(null);
            Assert.Equal("gpt-4o-mini", tokenizer.Name);
        }

        [Fact]
        public void CountTokens_ShouldReturnZero_WhenTextIsNullOrEmpty()
        {
            var tokenizer = new OpenAITikTokenizer();
            Assert.Equal(0, tokenizer.CountTokens((string)null));
            Assert.Equal(0, tokenizer.CountTokens(string.Empty));
        }

        [Fact]
        public void CountTokens_ShouldReturnZero_WhenMessagesIsNull()
        {
            var tokenizer = new OpenAITikTokenizer();
            Assert.Equal(0, tokenizer.CountTokens((IEnumerable<(string role, string content)>)null));
        }

        [Fact]
        public void Encode_ShouldReturnEmptyList_WhenTextIsNullOrEmpty()
        {
            var tokenizer = new OpenAITikTokenizer();
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
            var tokenizer = new OpenAITikTokenizer();
            var result = tokenizer.Decode(null);
            Assert.Equal(string.Empty, result);
        }
    }
}
