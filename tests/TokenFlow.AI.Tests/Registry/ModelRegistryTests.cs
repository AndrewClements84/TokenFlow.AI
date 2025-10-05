using TokenFlow.AI.Registry;
using TokenFlow.Core.Models;

namespace TokenFlow.AI.Tests.Registry
{
    public class ModelRegistryTests
    {
        [Fact]
        public void Get_ShouldReturn_Model_WhenExists()
        {
            // Arrange
            var registry = new ModelRegistry();

            // Act
            var model = registry.Get("gpt-4o");

            // Assert
            Assert.NotNull(model);
            Assert.Equal("gpt-4o", model.Id);
            Assert.Equal("openai", model.Family);
            Assert.Equal("approx", model.TokenizerName);
            Assert.Equal(128000, model.MaxInputTokens);
        }

        [Fact]
        public void Get_ShouldThrow_WhenModelDoesNotExist()
        {
            // Arrange
            var registry = new ModelRegistry();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => registry.Get("non-existent-model"));
        }

        [Fact]
        public void TryGet_ShouldReturnTrue_WhenModelExists()
        {
            // Arrange
            var registry = new ModelRegistry();

            // Act
            ModelSpec model;
            bool found = registry.TryGet("claude-3-sonnet", out model);

            // Assert
            Assert.True(found);
            Assert.NotNull(model);
            Assert.Equal("claude-3-sonnet", model.Id);
        }

        [Fact]
        public void TryGet_ShouldReturnFalse_WhenModelDoesNotExist()
        {
            // Arrange
            var registry = new ModelRegistry();

            // Act
            ModelSpec model;
            bool found = registry.TryGet("fake-model", out model);

            // Assert
            Assert.False(found);
            Assert.Null(model);
        }

        [Fact]
        public void All_ShouldReturn_AllModels()
        {
            // Arrange
            var registry = new ModelRegistry();

            // Act
            var allModels = registry.All().ToList();

            // Assert
            Assert.NotEmpty(allModels);
            Assert.True(allModels.Count >= 2);
            Assert.Contains(allModels, m => m.Id == "gpt-4o");
            Assert.Contains(allModels, m => m.Id == "claude-3-sonnet");
        }
    }
}

