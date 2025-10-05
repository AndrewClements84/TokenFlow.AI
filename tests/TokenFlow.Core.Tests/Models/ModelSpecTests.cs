using Xunit;
using TokenFlow.Core.Models;

namespace TokenFlow.Core.Tests.Models
{
    public class ModelSpecTests
    {
        [Fact]
        public void Constructor_ShouldAssign_AllProperties()
        {
            // Arrange
            string id = "gpt-4o";
            string family = "openai";
            string tokenizer = "approx";
            int maxInput = 128000;
            int? maxOutput = 4096;
            decimal inputPrice = 0.01m;
            decimal outputPrice = 0.03m;

            // Act
            var model = new ModelSpec(
                id,
                family,
                tokenizer,
                maxInput,
                maxOutput,
                inputPrice,
                outputPrice);

            // Assert
            Assert.Equal(id, model.Id);
            Assert.Equal(family, model.Family);
            Assert.Equal(tokenizer, model.TokenizerName);
            Assert.Equal(maxInput, model.MaxInputTokens);
            Assert.Equal(maxOutput, model.MaxOutputTokens);
            Assert.Equal(inputPrice, model.InputPricePer1K);
            Assert.Equal(outputPrice, model.OutputPricePer1K);
        }

        [Fact]
        public void MaxOutputTokens_ShouldAllowNullValue()
        {
            // Arrange
            var model = new ModelSpec(
                "claude-3-sonnet",
                "anthropic",
                "approx",
                200000,
                null,
                0.008m,
                0.024m);

            // Act
            int? maxOutput = model.MaxOutputTokens;

            // Assert
            Assert.Null(maxOutput);
            Assert.Equal("claude-3-sonnet", model.Id);
            Assert.Equal("anthropic", model.Family);
            Assert.Equal("approx", model.TokenizerName);
        }
    }
}

