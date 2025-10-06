using TokenFlow.AI.Client;

namespace TokenFlow.AI.Tests.Client
{
    public class TokenFlowClientTests
    {
        [Fact]
        public void AnalyzeText_ShouldReturn_TokenCountAndCost()
        {
            var client = new TokenFlowClient("gpt-4o");
            var result = client.AnalyzeText("Hello TokenFlow.AI!");

            Assert.True(result.TokenCount > 0);
            Assert.True(result.EstimatedCost > 0);
            Assert.Equal("gpt-4o", result.ModelId);
        }

        [Fact]
        public void AnalyzeText_ShouldReturnZero_ForEmptyInput()
        {
            var client = new TokenFlowClient("gpt-4o");
            var result = client.AnalyzeText(string.Empty);

            Assert.Equal(0, result.TokenCount);
            Assert.Equal(0m, result.EstimatedCost);
        }

        [Fact]
        public void ChunkText_ShouldSplitText_WhenExceedsTokenLimit()
        {
            var client = new TokenFlowClient("gpt-4o");
            string text = string.Join(" ", Enumerable.Repeat("TokenFlow", 200));

            var chunks = client.ChunkText(text, 10);

            Assert.True(chunks.Count > 1);
            Assert.All(chunks, c => Assert.False(string.IsNullOrEmpty(c)));
        }

        [Fact]
        public void GetModel_ShouldReturnSelectedModel()
        {
            var client = new TokenFlowClient("gpt-4o");
            var model = client.GetModel();

            Assert.NotNull(model);
            Assert.Equal("gpt-4o", model.Id);
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenModelNotFound()
        {
            Assert.Throws<System.ArgumentException>(() => new TokenFlowClient("fake-model"));
        }
    }
}

