using TokenFlow.Core.Models;

namespace TokenFlow.Core.Tests.Models
{
    public class TokenCountResultTests
    {
        [Fact]
        public void Constructor_ShouldAssign_AllPropertiesCorrectly()
        {
            // Arrange
            int prompt = 100;
            int completion = 200;
            int total = 300;

            // Act
            var result = new TokenCountResult(prompt, completion, total);

            // Assert
            Assert.Equal(prompt, result.PromptTokens);
            Assert.Equal(completion, result.CompletionTokens);
            Assert.Equal(total, result.TotalTokens);
        }

        [Fact]
        public void TotalTokens_ShouldReflectSum_WhenPassedAsSum()
        {
            // Arrange
            var result = new TokenCountResult(5, 10, 15);

            // Act
            int total = result.TotalTokens;

            // Assert
            Assert.Equal(15, total);
            Assert.True(total == result.PromptTokens + result.CompletionTokens);
        }
    }
}

