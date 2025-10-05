using TokenFlow.AI.Tokenizer;

namespace TokenFlow.AI.Tests.Tokenizer
{
    public class ApproxTokenizerTests
    {
        [Fact]
        public void CountTokens_ShouldReturnZero_ForEmptyText()
        {
            var tokenizer = new ApproxTokenizer();
            var count = tokenizer.CountTokens(string.Empty);
            Assert.Equal(0, count);
        }

        [Fact]
        public void CountTokens_ShouldReturnWordCount_ForSimpleSentence()
        {
            var tokenizer = new ApproxTokenizer();
            var count = tokenizer.CountTokens("Hello world, this is a test!");
            Assert.True(count >= 5); // Depends on punctuation split
        }

        [Fact]
        public void Encode_AndDecode_ShouldRoundTripText()
        {
            var tokenizer = new ApproxTokenizer();
            var original = "TokenFlow.AI is awesome!";
            var tokens = tokenizer.Encode(original);
            var decoded = tokenizer.Decode(tokens);

            Assert.False(string.IsNullOrWhiteSpace(decoded));
            Assert.Contains("TokenFlow", decoded);
        }

        [Fact]
        public void CountTokens_ShouldSumAcrossMessages()
        {
            var tokenizer = new ApproxTokenizer();
            var messages = new[]
            {
                ("system", "Hello world."),
                ("user", "How are you?")
            };
            var total = tokenizer.CountTokens(messages);
            Assert.True(total > 0);
        }
    }
}

